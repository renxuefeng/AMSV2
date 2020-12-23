using amsv2.Core.Dependency;
using amsv2.Core.EntityFrameworkCore;
using amsv2.Model.Dto;
using amsv2.Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace amsv2.Repository.IRepositories
{
    public interface IRepository: ITransientDependency
    {

    }
    public interface IRepository<TEntity, TPrimaryKey> : IScopeDependency //where TEntity : Entity<TPrimaryKey>//IRepository where TEntity : Entity<TPrimaryKey>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAllList();
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllList(Func<IIncludable<TEntity>, IIncludable> includes);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetAllList(Expression<Func<TEntity, bool>> predicate, Func<IIncludable<TEntity>, IIncludable> includes);
        /// <summary>
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> Get(TPrimaryKey id);
        /// <summary>
        /// 根据ID获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<TEntity> Get(TPrimaryKey id,Func<IIncludable<TEntity>, IIncludable> includes); 
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] Includes);
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<TEntity> Update(TEntity entity, bool autoSave = true);

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <returns></returns>
        Task<TEntity> InsertOrUpdate(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        Task Delete(TEntity entity, bool autoSave = true);
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where"></param>
        /// <param name="autoSave"></param>
        Task Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        Task<PageModel> LoadPageList(PageModel pageModel, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面条目</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序条件</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        Task<PageModel> LoadPageList(PageModel pageModel, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order, bool asc = true);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面条目</param>
        /// <param name="where">查询条件</param>
        /// <param name="includes">数据关系</param>
        /// <param name="order">排序条件</param>
        /// <param name="asc">是否正序</param>
        /// <returns></returns>
        Task<PageModel> LoadPageList(PageModel pageModel, Expression<Func<TEntity, bool>> where, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, object>> order, bool asc = true);
        Task Save();

    }
    public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : Entity
    {

    }
}
