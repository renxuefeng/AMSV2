using amsv2.Model.Entitys;
using amsv2.Repository.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace amsv2.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 事务
        /// </summary>
        IDbContextTransaction DbTransaction { get; }

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}
