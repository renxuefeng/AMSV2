using amsv2.Model.Dto;
using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace amsv2.Service.UserService
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IModuleRepository _moduleRepository;
        public UserInfoService(IUserRepository userRepository, IRoleRepository roleRepository, IModuleRepository moduleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task<UserInfo> CheckUserPassword(string user, string password)
        {
            return await _userRepository.Get(x => x.UserName == user && x.Password == password,x=>x.Roles,x=>x.Modules);
        }

        public async Task Delete(UserInfo ui)
        {
            await _userRepository.Delete(ui);
        }

        public async Task<List<UserInfo>> GetUserList(Expression<Func<UserInfo, bool>> where = null)
        {
            return await _userRepository.GetAllList(where);
        }

        public async Task<PageModel> GetUserList(int startPage, int pageSize, Expression<Func<UserInfo, bool>> where = null, Expression<Func<UserInfo, object>> order = null)
        {
            return await _userRepository.LoadPageList(startPage, pageSize, where, order);
        }

        public async Task<string> GetUserPermission(UserInfo userInfo)
        {
            List<int> moduleList = new List<int>();
            if (userInfo.Roles != null && userInfo.Roles.Count > 0)
            {
                foreach (var v in userInfo.Roles)
                {
                    var roleInfo = await _roleRepository.Get(x => x.Id == v.RoleId, x => x.Modules);
                    if (roleInfo.Modules != null && roleInfo.Modules.Count > 0)
                    {
                        foreach(var vv in roleInfo.Modules)
                        {
                            var moduleInfo = await _moduleRepository.Get(vv.Id);
                            if (moduleInfo != null)
                                moduleList.Add(moduleInfo.ModuleNO);
                        }
                    }
                        
                }
            }
            if (userInfo.Modules != null && userInfo.Modules.Count > 0)
            {
                foreach (var v in userInfo.Modules)
                {
                    if (!moduleList.Contains(v.Module.ModuleNO))
                        moduleList.Add(v.Module.ModuleNO);
                }
            }
            return string.Join(",", moduleList);
        }

        public async Task<UserInfo> Insert(UserInfo ui)
        {
            return await _userRepository.Insert(ui);
        }

        public async Task<UserInfo> Update(UserInfo ui)
        {
            return await _userRepository.Update(ui);
        }
    }
}
