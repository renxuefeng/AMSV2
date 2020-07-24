using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Repository.Repositories
{
    public class UserRepository : RepositoryBase<UserInfo>, IUserRepository
    {
        public UserRepository(AMSV2DbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
