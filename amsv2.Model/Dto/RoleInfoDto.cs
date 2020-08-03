using amsv2.Model.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace amsv2.Model.Dto
{
    public class RoleInfoDto
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required]
        [StringLength(20, ErrorMessage = "角色名称长度不能超过20个字符")]
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        [StringLength(20, ErrorMessage = "备注长度不能超过20个字符")]
        public string Description { get; set; }
        public List<UserInRole> Users { get; set; }
        public List<RoleInModule> Modules { get; set; }
        ///// <summary>
        ///// 用户IDS
        ///// </summary>
        //public List<long> userIDS { get; set; }
        ///// <summary>
        ///// 模块权限IDS
        ///// </summary>
        //public List<long> moduleIDS { get; set; }
    }
}
