using amsv2.Core.Dependency;
using amsv2.Model.Dto;
using amsv2.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace amsv2.Service.UserService
{
    public interface IUserInfoService : IScopeDependency
    {
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<string> GetUserPermission(UserInfo userInfo);
        /// <summary>
        /// 检查用户密码是否正确
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<UserInfo> CheckUserPassword(string user, string password);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<List<UserInfo>> GetUserList(Expression<Func<UserInfo, bool>> where = null);
        /// <summary>
        /// 获取用户分页数据
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowCount"></param>
        /// <param name="pageCount"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<PageModel> GetUserList(PageModel pageModel, Expression<Func<UserInfo, bool>> where = null, Expression<Func<UserInfo, object>> order = null);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        Task<UserInfo> Insert(UserInfo ui);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ui"></param>
        /// <returns></returns>
        Task<UserInfo> Update(UserInfo ui);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ui"></param>
        Task Delete(UserInfo ui);
    }
}
