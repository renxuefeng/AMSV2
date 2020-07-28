using amsv2.Core.EntityFrameworkCore;
using amsv2.Model.Dto;
using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace amsv2.Repository.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        protected readonly AMSV2DbContext _dbContext;
        public RepositoryBase(AMSV2DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<TEntity>> GetAllList()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
        public async Task<List<TEntity>> GetAllList(Expression<Func<TEntity, bool>> predicate, Func<IIncludable<TEntity>, IIncludable> includes)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).IncludeMultiple(includes).ToListAsync();
        }
        public async Task<PageModel> LoadPageList(PageModel pageModel, Expression<Func<TEntity, bool>> where)
        {
            return await LoadPageList(pageModel, where, null);
        }
        public async Task<PageModel> LoadPageList(PageModel pageModel, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order, bool asc = true)
        {
            return await LoadPageList(pageModel, where,null, order,asc);
        }
        public async Task<PageModel> LoadPageList(PageModel pageModel, Expression<Func<TEntity, bool>> where, Func<IIncludable<TEntity>, IIncludable> includes, Expression<Func<TEntity, object>> order, bool asc = true)
        {
            var result = from p in _dbContext.Set<TEntity>()
                         select p;
            if (where != null)
                result = result.Where(where);
            if (includes != null)
                result = result.IncludeMultiple(includes);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            pageModel.rowCount = result.Count();
            pageModel.pageCount = pageModel.rowCount / pageModel.pageSize;
            if (pageModel.rowCount % pageModel.pageSize != 0)
            {
                //如果余数不为0总页数就加上1
                pageModel.pageCount = pageModel.pageCount + 1;
            }
            // 请求的起始页大于总页数则取最后一页数据
            if (pageModel.pageCount > 0 && pageModel.startPage > pageModel.pageCount)
            {
                pageModel.startPage = pageModel.pageCount;
            }
            pageModel.data = await result.Skip((pageModel.startPage - 1) * pageModel.pageSize).Take(pageModel.pageSize).ToListAsync();
            return pageModel;
        }

        public async Task<TEntity> Get(TPrimaryKey id)
        {
            return await _dbContext.Set<TEntity>().SingleAsync(CreateEqualityExpressionForId(id));
        }
        public async Task<TEntity> Insert(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Add(entity);
            if (autoSave)
                await Save();
            return entity;
        }
        public async Task<TEntity> Update(TEntity entity, bool autoSave = true)
        {
            var obj = await Get(entity.Id);
            EntityToEntity(entity, obj);
            if (autoSave)
                await Save();
            return entity;
        }

        public async Task<TEntity> InsertOrUpdate(TEntity entity, bool autoSave = true)
        {
            if (await Get(entity.Id) != null)
                return await Update(entity, autoSave);
            return await Insert(entity, autoSave);
        }
        public async Task Delete(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
                await Save();
        }

        public async Task Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it => _dbContext.Set<TEntity>().Remove(it));
            if (autoSave)
                await Save();
        }

        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var mItem in typeof(T).GetProperties())
            {
                object[] o = mItem.GetCustomAttributes(typeof(NotMappedAttribute), true);
                if (o.Count() == 0)
                {
                    //if (mItem.Name != "down" && mItem.Name != "up"&& mItem.Name != "Down" && mItem.Name != "Up")
                    mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
                }
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] Includes)
        {
            return await _dbContext.Set<TEntity>().IncludeMultiple(Includes).SingleOrDefaultAsync(predicate);
        }

        public async Task<TEntity> Get(TPrimaryKey id, Func<IIncludable<TEntity>, IIncludable> includes)
        {
            return await _dbContext.Set<TEntity>()
        .IncludeMultiple(includes)
        .SingleOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public async Task<List<TEntity>> GetAllList(Func<IIncludable<TEntity>, IIncludable> includes)
        {
            return await _dbContext.Set<TEntity>().IncludeMultiple(includes).ToListAsync();
        }

    }
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity,long> where TEntity : Entity
    {
        public RepositoryBase(AMSV2DbContext dbContext) : base(dbContext)
        {
        }
    }
}
