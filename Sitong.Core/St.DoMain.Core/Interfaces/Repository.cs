using Microsoft.EntityFrameworkCore;
using St.DoMain.Interfaces;
using St.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St.DoMain.Core.Interfaces
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _StDb;
        private readonly DbSet<TEntity> _StSet;

        public Repository(IServiceProvider provider)
        {
            UnitOfWork = (provider.GetService(typeof(IUnitOfWork)) as IUnitOfWork);
            _StDb = UnitOfWork.GetDb();
            _StSet = _StDb.Set<TEntity>();
        }
        /// <summary>
        /// 工作单元
        /// </summary>
        public virtual IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 不追踪实体获取<see cref="IQueryable"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        public virtual IQueryable<TEntity> AsNoTracking()
            => _StSet.AsNoTracking();
        /// <summary>
        /// 追踪实体获取<see cref="IQueryable"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        public virtual IQueryable<TEntity> AsTracking()
            => _StSet.AsTracking();

        public bool Delete(TEntity model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByID(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIDAsync(object id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(TEntity model)
        {
            throw new NotImplementedException();
        }

        public bool Insert(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public bool Updata(TEntity model)
        {
            throw new NotImplementedException();
        }

        public bool Updata(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdataAsync(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdataAsync(IEnumerable<TEntity> model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 释放资源 
        /// </summary>
        public void Dispose()
        {
            _StDb.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
