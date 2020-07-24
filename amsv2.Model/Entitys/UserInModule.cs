using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Model.Entitys
{
    /// <summary>
    /// 用户权限关联表
    /// </summary>
    public class UserInModule : Entity
    {
        public long UserId { get; set; }
        public UserInfo User { get; set; }

        public long moduleId { get; set; }
        public ModuleInfo Module { get; set; }
    }
}
