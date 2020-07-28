using amsv2.Common;
using amsv2.Core.EntityFrameworkCore;
using amsv2.Core.Maps;
using amsv2.Model.Dto;
using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using amsv2.Service.UserService;
using AMSV2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AMSV2.Controllers
{
    public class UserController : BaseController
    {
        private readonly ResponseData _responseData;
        private readonly IUserInfoService _userInfoService;
        private readonly IUserRepository _userRepository;
        public UserController(ResponseData responseData, IUserInfoService userInfoService, IUserRepository userRepository)
        {
            _responseData = responseData;
            _userInfoService = userInfoService;
            _userRepository = userRepository;
        }
        /// <summary>
        /// 获取已登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        public async Task<ActionResult<ResponseData>> Get()
        {
            _responseData.Success = true;
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                UserInfo userInfo = await _userRepository.Get(x=>x.UserName == UserName);
                //UserInfo userInfo1 = await _userRepository.Get(userInfo.Id, ip => ip
                //.Include(y => y.Roles)
                //.ThenInclude(z => z.Role));
                if (userInfo.UserType == (int)UserTypeEnum.超级管理员)
                {
                    Dictionary<int, string> pList = new Dictionary<int, string>();
                    foreach (ModulesType foo in Enum.GetValues(typeof(ModulesType)))
                    {
                        pList.Add((int)foo, foo.ToString());
                    }
                    userInfo.Down["Modules"] = pList.Select(x => x.Key).ToArray();
                }
                else
                {
                    userInfo.Down["Modules"] = _userInfoService.GetUserPermission(userInfo);
                }
                _responseData.Data = userInfo;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户过期，请重新登录";
            }
            return _responseData;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserList")]
        public async Task<ActionResult<ResponseData>> GetUserList()
        {
            _responseData.Success = true;
            var uiList = await _userRepository.GetAllList(info=>info.Include(x=>x.Roles).ThenInclude(y=>y.Role));
            // 取用户角色名称下发前台
            uiList.ForEach(x =>
            {
                if (x.Roles != null && x.Roles?.Count > 0)
                {
                    x.Down["roleName"] = string.Join(',', x.Roles.Select(y => y.Role?.RoleName));
                }
                else
                {
                    x.Down["roleName"] = "";
                }
            });
            _responseData.Data = uiList;
            return _responseData;
        }
        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetUserListByPage")]
        public async Task<ActionResult<ResponseData>> GetUserListByPage(PageModel pageModel)
        {
            _responseData.Success = true;
            var uiList = await _userRepository.LoadPageList(pageModel,null,info => info.Include(x=>x.Roles).ThenInclude(y=>y.Role),null);
            // 取用户角色名称下发前台
            var list = uiList.data as List<UserInfo>;
            list.ForEach(x => {
                x.Down["roleName"] = string.Join(',', x.Roles.Select(y => y.Role?.RoleName));
            });
            _responseData.Data = uiList;
            return _responseData;
        }
        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="userInfoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseData>> Create(UserInfoDto userInfoDto)
        {
            if (userInfoDto != null && !string.IsNullOrWhiteSpace(userInfoDto.UserName))
            {
                UserInfo userInfo = new UserInfo();
                userInfoDto.MapTo(userInfo);
                var uiInfo = await _userInfoService.GetUserList(x => x.UserName == userInfo.UserName);
                if (uiInfo != null && uiInfo.Count > 0)
                {
                    _responseData.Success = false;
                    _responseData.Message = "用户已存在";
                }
                else
                {
                    if (userInfo.Up.ContainsKey("roleIDS") && !string.IsNullOrWhiteSpace(userInfo.Up["roleIDS"].ToString()))
                    {
                        List<long> roleIDS = JsonConvert.DeserializeObject<List<long>>(userInfo.Up["roleIDS"].ToString());
                        if (roleIDS != null && roleIDS.Count > 0)
                        {
                            userInfo.Roles = new List<UserInRole>();
                            roleIDS?.ForEach(x => userInfo.Roles?.ToList().Add(new UserInRole() { RoleId = x }));
                        }
                    }
                    userInfo.CreateUserID = long.Parse(User.FindFirst(x => x.Type == ClaimTypes.PrimarySid).Value);
                    userInfo.CreateUserTime = DateTime.Now;
                    var user = await _userInfoService.Insert(userInfo);
                    _responseData.Success = true;
                    _responseData.Data = user;
                }
            }
            return _responseData;
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="userInfoDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResponseData>> Update(UserInfoDto userInfoDto)
        {
            if (userInfoDto != null && userInfoDto.id > 0)
            {
                var userInfo = await _userRepository.Get(userInfoDto.id,i=>i.Include(x=>x.Roles).ThenInclude(x=>x.Role));
                if (userInfo != null)
                {
                    userInfoDto.MapTo(userInfo);
                    if (userInfoDto.RoleIDS != null && userInfoDto.RoleIDS.Count > 0)
                    {
                        userInfo.Roles = new List<UserInRole>();
                        userInfoDto.RoleIDS.ForEach(x => userInfo.Roles?.Add(new UserInRole() { RoleId = x,UserId = userInfo.Id}));
                    }
                    var user = await _userInfoService.Update(userInfo);
                    _responseData.Success = true;
                    _responseData.Data = user;
                }
                else
                {
                    _responseData.Success = false;
                    _responseData.Message = "用户不存在";
                }
            }
            return _responseData;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<ResponseData>> Delete(long id)
        {
            var info = await _userRepository.Get(id);
            if (info != null)
            {
                await _userInfoService.Delete(info);
                _responseData.Success = true;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "用户不存在";
            }
            return _responseData;
        }
    }
}
