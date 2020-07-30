using Microsoft.EntityFrameworkCore;
using St.DoMain.Interfaces;
using St.EfCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace St.DoMain.Core.Interfaces
{
    /// <summary>
    /// https://docs.microsoft.com/zh-cn/ef/core/saving/transactions Microsoft => 使用事务
    /// 工作单元实现类.
    /// </summary>
    public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
    {
        private readonly TDbContext _Db;

        public UnitOfWork(TDbContext dbContext)
        {
            _Db = dbContext;
        }

        /// <summary>
        /// 获取DbContext上下文
        /// </summary>
        /// <returns></returns>
        public DbContext GetDb()
        {
            if (_Db != null)
            {
                return _Db;
            }
            else
                throw new NullReferenceException("DbContext注入失败!");
        }


        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Task BeginTransactionAsync(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }


        public void RollBack()
        {
            throw new NotImplementedException();
        }

        public Task RollBackAsync()
        {
            throw new NotImplementedException();
        }
    }
}
