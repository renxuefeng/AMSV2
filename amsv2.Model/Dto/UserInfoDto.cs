using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace amsv2.Model.Dto
{
    public class UserInfoDto
    {
        public long id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(50)]
        [StringLength(14, ErrorMessage = "用户名长度不能超过14个字符")]
        public string UserName { get; set; }
        [MaxLength(50)]
        [StringLength(32, ErrorMessage = "密码长度不正确")]
        public string Password { get; set; }

        /**********************************************************************************************//**
         * @property    public string Name
         *
         * @brief   姓名
         *
         * @return  The name.
         **************************************************************************************************/

        [MaxLength(50)]
        [StringLength(20, ErrorMessage = "姓名长度不能超过20个字符")]
        public string Name { get; set; }

        /**********************************************************************************************//**
         * @property    public int Gender
         *
         * @brief   性别
         *
         * @return  The gender.
         **************************************************************************************************/

        [Required]
        public int Gender { get; set; }

        /**********************************************************************************************//**
         * @property    public int Status
         *
         * @brief   账户状态 启用or禁用
         *
         * @return  The status.
         **************************************************************************************************/

        [Required]
        public int Status { get; set; }

        /**********************************************************************************************//**
         * @property    public string UserPic
         *
         * @brief   用户头像
         *
         * @return  The user picture.
         **************************************************************************************************/

        //[Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string UserPic { get; set; }

        [Required]
        public int UserType { get; set; }

        /**********************************************************************************************//**
         * @property    public string WorkUnit
         *
         * @brief   工作单位
         *
         * @return  The work unit.
         **************************************************************************************************/

        [MaxLength(50)]
        [StringLength(30, ErrorMessage = "工作单位长度不能超过30个字符")]
        public string WorkUnit { get; set; }
        public List<long> RoleIDS { get; set; }
    }
}
