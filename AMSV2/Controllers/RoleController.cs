using amsv2.Core.EntityFrameworkCore;
using amsv2.Core.Maps;
using amsv2.Model.Dto;
using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using amsv2.Repository.UnitOfWork;
using amsv2.Service.UserService;
using AMSV2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMSV2.Controllers
{
    public class RoleController : BaseController
    {
        private readonly ResponseData _responseData;
        private readonly IUserInfoService _userInfoService;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoleController(ResponseData responseData, IUserInfoService userInfoService, IUserRepository userRepository, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _responseData = responseData;
            _userInfoService = userInfoService;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserList")]
        public async Task<ActionResult<ResponseData>> GetUserList()
        {
            _responseData.Success = true;
            var uiList = await _roleRepository.GetAllList(info => info.Include(x => x.Users).ThenInclude(y => y.User).Include(x => x.Modules).ThenInclude(y => y.moduleInfo));
            // 取用户角色名称下发前台
            uiList.ForEach(x =>
            {
                x.Down["users"] = x.Users?.Select(y => y.User);
                x.Down["modules"] = x.Modules?.Select(y => y.moduleInfo);
            });
            _responseData.Data = uiList;
            return _responseData;
        }
        /// <summary>
        /// 获取角色列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetUserListByPage")]
        public async Task<ActionResult<ResponseData>> GetUserListByPage(PageModel pageModel)
        {
            _responseData.Success = true;
            var uiList = await _roleRepository.LoadPageList(pageModel, null, info => info.Include(x => x.Users).ThenInclude(y => y.User).Include(x=>x.Modules).ThenInclude(y=>y.moduleInfo), null);
            // 取角色下的用户名称下发前台
            var list = uiList.data as List<RoleInfo>;
            list.ForEach(x => {
                x.Down["users"] = x.Users?.Select(y => y.User);
                x.Down["modules"] = x.Modules?.Select(y => y.moduleInfo);
            });
            _responseData.Data = uiList;
            return _responseData;
        }
        /// <summary>
        /// 新建角色
        /// </summary>
        /// <param name="roleInfoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseData>> Create(RoleInfoDto roleInfoDto)
        {
            RoleInfo roleInfo = new RoleInfo();
            roleInfoDto.MapTo(roleInfo);
            roleInfo.CreateDateTime = DateTime.Now;
            roleInfo.CreateUserName = UserName;
            _responseData.Data = await _roleRepository.Insert(roleInfo);
            _responseData.Success = true;
            return _responseData;
            //try
            //{
            //    _unitOfWork.BeginTransaction();
            //    RoleInfo roleInfo = new RoleInfo();
            //    roleInfoDto.MapTo(roleInfo);
            //    await _roleRepository.Insert(roleInfo);
            //    //await _roleRepository.Save();
            //    await _roleRepository.Insert(roleInfo);
            //    //await _roleRepository.Save();
            //    _unitOfWork.Commit();
            //}
            //catch (Exception e)
            //{
            //    _unitOfWork.Rollback();
            //}

        }
        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="roleInfoDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResponseData>> Update(RoleInfoDto roleInfoDto)
        {
            if (roleInfoDto != null && roleInfoDto.Id > 0)
            {
                var roleInfo = await _roleRepository.Get(roleInfoDto.Id, i => i.Include(x => x.Users).ThenInclude(x => x.User).Include(x => x.Modules).ThenInclude(y => y.moduleInfo));
                if (roleInfo != null)
                {
                    if (roleInfo.Users.Count > 0)
                        roleInfo.Users.Clear();
                    if (roleInfo.Modules.Count > 0)
                        roleInfo.Modules.Clear();
                    roleInfoDto.MapTo(roleInfo);
                    roleInfo.LastUpdateDateTime = DateTime.Now;
                    roleInfo.LastUpdateUserName = UserName;
                    var role = await _roleRepository.Update(roleInfo);
                    _responseData.Success = true;
                    _responseData.Data = role;
                }
                else
                {
                    _responseData.Success = false;
                    _responseData.Message = "角色不存在";
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
            var info = await _roleRepository.Get(id);
            if (info != null)
            {
                await _roleRepository.Delete(info);
                _responseData.Success = true;
            }
            else
            {
                _responseData.Success = false;
                _responseData.Message = "角色不存在";
            }
            return _responseData;
        }
    }
}
