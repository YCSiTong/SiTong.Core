using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace St.DoMain.Interfaces
{
    /// <summary>
    /// 工作单元 实现获取DbContext上下文以及事务
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDb();

        /// <summary>
        /// 执行委托<see cref="Action"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        void UseTransaction(Action action);

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<T> UseTransactionAsync<T>(Func<Task<T>> action);

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <returns><see cref="Task"/></returns>
        Task UseTransactionAsync(Func<Task> action);

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns><see cref="Task{TResult}"/></returns>
        Task<T> UseTransactionAsync<T>(Func<Task<T>> action, IsolationLevel isolationLevel);

        /// <summary>
        /// 执行委托<see cref="Func{TResult}"/>事务
        /// </summary>
        /// <param name="action">需要执行的代码块</param>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns><see cref="Task"/></returns>
        Task UseTransactionAsync(Func<Task> action, IsolationLevel isolationLevel);

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns></returns>
        Task BeginTransactionAsync(IsolationLevel isolationLevel);

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <returns></returns>
        Task RollBackAsync();

        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务级别</param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 事务提交
        /// </summary>
        void Commit();

        /// <summary>
        /// 事务回滚
        /// </summary>
        void RollBack();

    }
}
