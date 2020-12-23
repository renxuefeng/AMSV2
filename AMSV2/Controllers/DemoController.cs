using amsv2.Model.Entitys;
using amsv2.Repository;
using amsv2.Repository.IRepositories;
using amsv2.Repository.UnitOfWork;
using AMSV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMSV2.Controllers
{
    public class DemoController : BaseController
    {
        private readonly ResponseData _responseData;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<AMSV2DbContext> _unitOfWork;
        public DemoController(ResponseData responseData, IUserRepository userRepository, IUnitOfWork<AMSV2DbContext> unitOfWork, IRoleRepository roleRepository)
        {
            _responseData = responseData;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ResponseData>> TestTran()
        {
            //using (var tran = _unitOfWork.BeginTransaction())
            //{
            //    await _unitOfWork.GetRepository<RoleInfo>().Insert(new RoleInfo() { RoleName = "aaabbb", CreateUserName = "1", CreateDateTime = DateTime.Now });
            //    await _unitOfWork.GetRepository<RoleInfo>().Insert(new RoleInfo() { RoleName = "bbbaaa", CreateUserName = "1", CreateDateTime = DateTime.Now });
            //    await _unitOfWork.GetRepository<UserInfo>().Insert(new UserInfo() { Id = 1, UserName = "333", Gender = 1, Status = 1, UserType = 1, CreateUserID = 1, CreateUserTime = DateTime.Now });
            //    await tran.CommitAsync();
            //};
            try
            {
                //_unitOfWork.BeginTransaction();
                await _roleRepository.Insert(new RoleInfo() { RoleName = "aaabbb", CreateUserName = "1", CreateDateTime = DateTime.Now });
                await _roleRepository.Insert(new RoleInfo() { RoleName = "bbbaaa", CreateUserName = "1", CreateDateTime = DateTime.Now });
                //await _userRepository.Insert(new UserInfo() { UserName = "111", Gender = 1, Status = 1, UserType = 1, CreateUserID = 1, CreateUserTime = DateTime.Now }, false);
                //await _userRepository.Insert(new UserInfo() { UserName = "222", Gender = 1, Status = 1, UserType = 1, CreateUserID = 1, CreateUserTime = DateTime.Now }, false);
                //await _userRepository.Insert(new UserInfo() { UserName = "333", Gender = 1, Status = 1, UserType = 1, CreateUserID = 1, CreateUserTime = DateTime.Now }, false);
                await _userRepository.Insert(new UserInfo() { Id = 1, UserName = "333", Gender = 1, Status = 1, UserType = 1, CreateUserID = 1, CreateUserTime = DateTime.Now });
                await _userRepository.Save();
                //_unitOfWork.Commit();
            }
            catch (Exception e)
            {
                //await _userRepository.Save();
                //_unitOfWork.Rollback();
            }
            return _responseData;
        }
    }
}
