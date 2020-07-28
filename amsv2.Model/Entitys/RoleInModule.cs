using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace amsv2.Model.Entitys
{
    public class RoleInModule : Entity
    {
        public long RoleId { get; set; }
        [JsonIgnore]
        public RoleInfo roleInfo { get; set; }

        public long ModuleId { get; set; }
        [JsonIgnore]
        public ModuleInfo moduleInfo { get; set; }
    }
}
