using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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
        /// 回滚事务
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
