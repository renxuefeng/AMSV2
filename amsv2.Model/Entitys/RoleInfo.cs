using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace amsv2.Model.Entitys
{
    /**********************************************************************************************//**
 * @class   RoleInfo
 *
 * @brief   权限信息实体
 *
 * @author  rxf
 * @date    2017/12/25
 **************************************************************************************************/
    [Table("RoleInfo")]
    public class RoleInfo : Entity
    {
        /**********************************************************************************************//**
         * @property    public string RoleName
         *
         * @brief   角色名称
         *
         * @return  The name of the role.
         **************************************************************************************************/
        [Required]
        //[Remote("CheckUserName", "Role", ErrorMessage = "角色名称不能重复")]
        [MaxLength(50)]
        [StringLength(20, ErrorMessage = "角色名称长度不能超过20个字符")]
        public string RoleName { get; set; }
        /**********************************************************************************************//**
         * @property    public string Description
         *
         * @brief   角色描述
         *
         * @return  The description.
         **************************************************************************************************/
        [MaxLength(100)]
        [StringLength(20, ErrorMessage = "备注长度不能超过20个字符")]
        public string Description { get; set; }
        [Required]
        public DateTime CreateDateTime { get; set; }
        [Required]
        public long CreateUserID { get; set; }
        [JsonIgnore]
        public List<UserInRole> Users { get; set; }
        [JsonIgnore]
        public List<RoleInModule> Modules { get; set; }
    }
}
