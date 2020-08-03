using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace amsv2.Repository.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private IDbContextTransaction _trans = null;
        protected readonly AMSV2DbContext _dbContext;
        /// <summary>
        /// 事务
        /// </summary>
        public IDbContextTransaction DbTransaction { get { return _trans; } }

        private IDbConnection _connection;
        /// <summary>
        /// 数据连接
        /// </summary>
        public IDbConnection DbConnection { get { return _connection; } }

        public UnitOfWork(AMSV2DbContext dbContext)
        {
            _dbContext = dbContext;
            //var connectionString = configuration.GetConnectionString("SqlConnection");
            ////_connection = new MySqlConnection(connectionString);//MySqlConnector
            //_connection = new ProfiledDbConnection(new MySqlConnection(connectionString), MiniProfiler.Current);
            //_connection.Open();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            _trans = _dbContext.Database.BeginTransaction();
        }
        /// <summary>
        /// 完成事务
        /// </summary>
        public void Commit() => _trans?.Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback() => _trans?.Rollback();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _trans?.Dispose();
                _connection?.Dispose();
            }

            _trans = null;
            _connection = null;
            _disposed = true;
        }


    }
}
