using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Model.Entitys
{
    public class RoleInModule : Entity
    {
        public long RoleId { get; set; }
        public RoleInfo roleInfo { get; set; }

        public long ModuleId { get; set; }
        public ModuleInfo moduleInfo { get; set; }
    }
}
