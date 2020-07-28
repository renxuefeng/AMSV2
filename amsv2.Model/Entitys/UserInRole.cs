using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace amsv2.Model.Entitys
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserInRole : Entity
    {
        public long UserId { get; set; }
        [JsonIgnore]
        public UserInfo User { get; set; }
        public long RoleId { get; set; }
        [JsonIgnore]
        public RoleInfo Role { get; set; }
    }
}
