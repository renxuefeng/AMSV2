using amsv2.Core.Dependency;
using amsv2.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Repository.IRepositories
{
    public interface IModuleRepository : IRepository<ModuleInfo>, IScopeDependency
    {
    }
}
