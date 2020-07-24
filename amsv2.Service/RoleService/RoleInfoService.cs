using amsv2.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Service.RoleService
{
    public class RoleInfoService : IRoleInfoService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleInfoService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
    }
}
