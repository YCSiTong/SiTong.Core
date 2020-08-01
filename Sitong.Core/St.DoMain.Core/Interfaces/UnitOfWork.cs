using Microsoft.EntityFrameworkCore;
using St.DoMain.Interfaces;
using St.Extensions;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace St.DoMain.Core.Interfaces
{
    /// <summary>
    /// 工作单元实现类.
    /// https://docs.microsoft.com/zh-cn/ef/core/saving/transactions Microsoft => 使用事务
    /// </summary>
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _Db;// 上下文对象

        public UnitOfWork(TDbContext dbContext)
        {
            _Db = dbContext;
        }

        private DbTransaction _DbTransaction;

        private bool _IsCommit = false;

        /// <summary>
        /// 获取DbContext上下文
        /// </summary>
        /// <returns></returns>
        public DbContext GetDb()
        {
            if (_Db.IsNotNull())
                return _Db;
            throw new NullReferenceException("DbContext注入失败!");
        }

        /// <summary>
        /// 执行委托<see cref="Action"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        public void UseTransaction(Action action)
        {
            action.NotNull(nameof(Action));

            BeginTransaction();
            action.Invoke();
            Commit();
        }

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<T> UseTransactionAsync<T>(Func<Task<T>> action)
        {
            action.NotNull(nameof(Func<Task<T>>));

            await BeginTransactionAsync();
            var result = await action.Invoke();
            await CommitAsync();
            return result;
        }

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <returns><see cref="Task"/></returns>
        public async Task UseTransactionAsync(Func<Task> action)
        {
            action.NotNull(nameof(Func<Task>));

            await BeginTransactionAsync();
            await action.Invoke();
            await CommitAsync();
        }

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        public async Task<T> UseTransactionAsync<T>(Func<Task<T>> action, IsolationLevel isolationLevel)
        {
            action.NotNull(nameof(Func<Task<T>>));

            await BeginTransactionAsync(isolationLevel);
            var result = await action.Invoke();
            await CommitAsync();
            return result;
        }

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns><see cref="Task"/></returns>
        public async Task UseTransactionAsync(Func<Task> action, IsolationLevel isolationLevel)
        {
            action.NotNull(nameof(Func<Task>));

            await BeginTransactionAsync(isolationLevel);
            await action.Invoke();
            await CommitAsync();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public virtual async Task BeginTransactionAsync()
        {
            if (_DbTransaction.Connection == null)
            {
                if (_DbTransaction.Connection.State != ConnectionState.Open)
                {
                    await _DbTransaction.Connection.OpenAsync();
                }
                _DbTransaction = await _DbTransaction.Connection.BeginTransactionAsync();
            }

            await _Db.Database.UseTransactionAsync(_DbTransaction);
            _IsCommit = true;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns></returns>
        public virtual async Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            if (_DbTransaction.Connection == null)
            {
                if (_DbTransaction.Connection.State != ConnectionState.Open)
                    await _DbTransaction.Connection.OpenAsync();
                _DbTransaction = await _DbTransaction.Connection.BeginTransactionAsync(isolationLevel);
            }

            await _Db.Database.UseTransactionAsync(_DbTransaction);
            _IsCommit = true;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public virtual async Task CommitAsync()
        {
            if (_DbTransaction == null || !_IsCommit)
                return;

            try
            {
                await _DbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                if (_DbTransaction?.Connection != null)
                    await _DbTransaction.RollbackAsync();
                throw ex;
            }
            finally
            {
                if (_Db.Database.CurrentTransaction != null)
                    await _Db.Database.CurrentTransaction.DisposeAsync();
                _IsCommit = false;
            }


        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        /// <returns></returns>
        public virtual async Task RollBackAsync()
        {
            if (_DbTransaction.Connection != null)
            {
                await _DbTransaction.RollbackAsync();
            }

            if (_Db.Database.CurrentTransaction != null)
            {
                await _Db.Database.CurrentTransaction.DisposeAsync();
            }
            _IsCommit = false;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public virtual void BeginTransaction()
        {
            if (_DbTransaction.Connection == null)
            {
                if (_DbTransaction.Connection.State != ConnectionState.Open)
                {
                    _DbTransaction.Connection.Open();
                }
                _DbTransaction = _DbTransaction.Connection.BeginTransaction();
            }
            _Db.Database.UseTransaction(_DbTransaction);
            _IsCommit = true;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务级别</param>
        public virtual void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_DbTransaction.Connection == null)
            {
                if (_DbTransaction.Connection.State != ConnectionState.Open)
                    _DbTransaction.Connection.Open();
                _DbTransaction = _DbTransaction.Connection.BeginTransaction(isolationLevel);
            }

            _Db.Database.UseTransaction(_DbTransaction);
            _IsCommit = true;
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public virtual void Commit()
        {
            if (_DbTransaction == null || !_IsCommit)
                return;
            try
            {
                _DbTransaction.Commit();

            }
            catch (Exception ex)
            {
                if (_DbTransaction?.Connection != null)
                    _DbTransaction.Rollback();
                throw ex;
            }
            finally
            {
                if (_Db.Database.CurrentTransaction != null)
                    _Db.Database.CurrentTransaction.Dispose();
                _IsCommit = false;
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public virtual void RollBack()
        {
            if (_DbTransaction.Connection != null)
                _DbTransaction.Rollback();

            if (_Db.Database.CurrentTransaction != null)
                _Db.Database.CurrentTransaction.Dispose();

            _IsCommit = false;
        }

    }
}
