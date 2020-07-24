using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Repository.Repositories
{
    public class RoleRepository : RepositoryBase<RoleInfo>, IRoleRepository
    {
        public RoleRepository(AMSV2DbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
