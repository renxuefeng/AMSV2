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
            roleInfo.CreateUser = UserName;
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
    }
}
