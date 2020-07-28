using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace amsv2.Model.Entitys
{
    /// <summary>
    /// 菜单模块表
    /// </summary>
    public class ModuleInfo : Entity
    {
        /// <summary>
        /// 模块编号
        /// </summary>
        public int ModuleNO { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required]
        public string ModuleName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool isEnable { get; set; }
        [JsonIgnore]
        public List<UserInModule> Users { get; set; }
        [JsonIgnore]
        public List<RoleInModule> Roles { get; set; }
    }
}
