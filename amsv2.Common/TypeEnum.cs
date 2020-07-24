using System;
using System.Collections.Generic;
using System.Text;

namespace amsv2.Common
{
    public enum ModulesType
    {
        系统管理_用户管理_新建 = 1,
        系统管理_用户管理_编辑,
        系统管理_用户管理_删除,
        系统管理_用户管理_修改密码,
        系统管理_用户管理_查看,
        系统管理_角色管理_查看,
        系统管理_角色管理_新增,
        系统管理_角色管理_编辑,
        系统管理_角色管理_删除,
    }
    /// <summary>
    /// 用户类型枚举
    /// </summary>
    public enum UserTypeEnum
    {
        普通用户,
        超级管理员,
    }
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderType
    {
        男 = 0,
        女
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum UserStatus
    {
        启用,
        禁用
    }
}
