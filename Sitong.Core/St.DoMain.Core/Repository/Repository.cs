using Microsoft.EntityFrameworkCore;
using St.DoMain.Entity;
using St.DoMain.Entity.Audited;
using St.DoMain.Repository;
using St.DoMain.UnitOfWork;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace St.DoMain.Core.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : IAggregateRoot<TKey>
    {
        private readonly DbContext _StDb;// 上下文对象
        private readonly DbSet<TEntity> _Entities;// 具体操作对象

        public Repository(IServiceProvider provider)
        {
            UnitOfWork = provider.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            _StDb = UnitOfWork.GetDb();
            _Entities = _StDb.Set<TEntity>();
        }

        private bool Save()
            => _StDb.SaveChanges() > 0;
        private async Task<bool> SaveAsync()
            => await _StDb.SaveChangesAsync() > 0;


        /// <summary>
        /// 工作单元
        /// </summary>
        public virtual IUnitOfWork UnitOfWork { get; }

        #region Query

        /// <summary>
        /// 不追踪实体获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        public virtual IQueryable<TEntity> AsNoTracking() => _Entities.AsNoTracking();

        /// <summary>
        /// 追踪实体获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        public virtual IQueryable<TEntity> AsTracking() => _Entities;

        /// <summary>
        /// 根据主键<typeparamref name="TKey"/>获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        public virtual TEntity GetById(TKey key) => _Entities.Find(key);

        /// <summary>
        /// 根据主键<typeparamref name="TKey"/>异步获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(TKey key) => await _Entities.FindAsync(key);

        #endregion

        #region Delete

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Delete(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            _Entities.Remove(model);
            return Save();
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Delete(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(IEnumerable<TEntity>));

            _Entities.RemoveRange(model);
            return Save();
        }

        /// <summary>
        /// 异步删除单条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> DeleteAsync(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            _Entities.Remove(model);
            return await SaveAsync();
        }

        /// <summary>
        /// 异步批量删除数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> DeleteAsync(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(IEnumerable<TEntity>));
            _Entities.RemoveRange(model);
            return await SaveAsync();
        }

        #endregion

        #region Insert

        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Insert(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            _Entities.Add(model);
            return Save();
        }

        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Insert(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(IEnumerable<TEntity>));
            _Entities.AddRange(model);
            return Save();
        }

        /// <summary>
        /// 异步新增单条数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> InsertAsync(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            await _Entities.AddAsync(model);
            return await SaveAsync();
        }

        /// <summary>
        /// 异步批量新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> InsertAsync(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(IEnumerable<TEntity>));
            await _Entities.AddRangeAsync(model);
            return await SaveAsync();
        }

        #endregion

        #region Update
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Updata(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            _Entities.Update(model);
            return Save();
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Updata(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(IEnumerable<TEntity>));
            _Entities.UpdateRange(model);
            return Save();
        }

        /// <summary>
        /// 异步更新单条数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> UpdataAsync(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            _Entities.Update(model);
            return await SaveAsync();
        }

        /// <summary>
        /// 异步批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> UpdataAsync(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(TEntity));
            _Entities.UpdateRange(model);
            return await SaveAsync();
        }

        #endregion

        private bool _Disposed = false; // 避免重复手动释放

        /// <summary>
        /// 释放资源 
        /// </summary>
        public void Dispose()
        {
            if (_Disposed)
                return;

            _StDb.Dispose();
            GC.SuppressFinalize(this);
            _Disposed = true;
        }
        /// <summary>
        /// 异步释放资源
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync()
        {
            if (_Disposed)
                return;
            await _StDb.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        #region CheckAudited


        //TODO:创建审计过滤添加信息.
        //private TEntity CheckInsert(TEntity model)
        //{
        //    if (model is ICreationAudited)
        //    {

        //    }
            
        //}

        #endregion



    }
}
