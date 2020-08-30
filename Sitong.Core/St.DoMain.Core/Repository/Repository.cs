using Microsoft.EntityFrameworkCore;
using St.DoMain.Entity.Audited;
using St.DoMain.Identity;
using St.DoMain.Repository;
using St.DoMain.UnitOfWork;
using St.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;

namespace St.DoMain.Core.Repository
{
    public class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class
        where TPrimaryKey : struct
    {
        private readonly DbContext _StDb;// 上下文对象
        private readonly DbSet<TEntity> _Entities;// 具体操作对象
        private readonly IdentityInfo _IdentityInfo;// 获取当前登录角色相关信息

        public Repository(IUnitOfWork unitOfWork, IdentityInfo identityInfo)
        {
            UnitOfWork = unitOfWork;
            _IdentityInfo = identityInfo;
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
        public virtual IQueryable<TEntity> AsNoTracking() => CheckIsFilterSoftDelete().AsNoTracking();

        /// <summary>
        /// 追踪实体获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        public virtual IQueryable<TEntity> AsTracking() => CheckIsFilterSoftDelete();

        /// <summary>
        /// 根据主键<typeparamref name="TPrimaryKey"/>获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        public virtual TEntity GetById(TPrimaryKey key)
        {
            var result = _Entities.Find(key);
            if (IsISoftDelete())
            {
                var softDelete = (ISoftDelete)result;
                if (softDelete.IsDeleted)
                    return default;
                else
                    return result;
            }
            return result;
        }

        /// <summary>
        /// 根据主键<typeparamref name="TPrimaryKey"/>异步获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(TPrimaryKey key)
        {
            var result = await _Entities.FindAsync(key);
            if (IsISoftDelete())
            {
                var softDelete = (ISoftDelete)result;
                if (softDelete.IsDeleted)
                    return default;
                else
                    return result;
            }
            return result;

        }

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
            CheckDelete(model);
            return Save();
        }

        /// <summary>
        /// 根据主键删除单条数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public virtual bool Delete(TPrimaryKey Id)
        {
            var model = GetById(Id);
            model.NotNull(nameof(model));
            CheckDelete(model);
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
            CheckDelete(model);
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
            CheckDelete(model);
            return await SaveAsync();
        }

        /// <summary>
        /// 根据主键异步删除单条数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(TPrimaryKey Id)
        {
            var model = await GetByIdAsync(Id);
            model.NotNull(nameof(model));
            CheckDelete(model);
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
            CheckDelete(model);
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
            CheckInsert(model);
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
            CheckInsert(model);
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
            CheckInsert(model);
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
            CheckInsert(model);
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
        public virtual bool Update(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            CheckUpdate(model);
            _Entities.Update(model);
            return Save();
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual bool Update(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(IEnumerable<TEntity>));
            CheckUpdate(model);
            _Entities.UpdateRange(model);
            return Save();
        }

        /// <summary>
        /// 异步更新单条数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> UpdateAsync(TEntity model)
        {
            model.NotNull(nameof(TEntity));
            CheckUpdate(model);
            _Entities.Update(model);
            return await SaveAsync();
        }

        /// <summary>
        /// 异步批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        public virtual async Task<bool> UpdateAsync(IEnumerable<TEntity> model)
        {
            model.NotNull(nameof(TEntity));
            CheckUpdate(model);
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

        /// <summary>
        ///  Check If Inherited `ISoftDelete` Interface
        ///  检查<typeparamref name="TEntity"/>是否实现<see cref="ISoftDelete"/>
        /// </summary>
        /// <returns></returns>
        private bool IsISoftDelete() => typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity));
        /// <summary>
        ///  Check If Inherited `ICreationAudited` Interface
        ///  检查<typeparamref name="TEntity"/>是否实现<see cref="ICreationAudited"/>
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private bool IsICreationAudited() => typeof(ICreationAudited<>).IsAssignableFrom(typeof(TEntity));
        /// <summary>
        ///  Check If Inherited `IModificationAudited` Interface
        ///  检查<typeparamref name="TEntity"/>是否实现<see cref="IModificationAudited"/>
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        private bool IsIModificationAudited() => typeof(IModificationAudited<>).IsAssignableFrom(typeof(TEntity));


        /// <summary>
        /// Check If Inherited `ISoftDelete` Interface,Query Time Filtering.
        /// 检查是否继承`ISoftDelete` 接口, 查询时过滤.
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        private IQueryable<TEntity> CheckIsFilterSoftDelete()
        {
            if (IsISoftDelete())
            {
                var rootNode = Expression.Parameter(typeof(TEntity), "op"); // 根节点
                var key = Expression.PropertyOrField(rootNode, "IsDeleted");// Key
                var val = Expression.Constant(false);// Val
                var conbine = Expression.Equal(key, val);//Key == Val
                var sqlWhere = Expression.Lambda<Func<TEntity, bool>>(conbine, rootNode);// Builder Lambda
                return _Entities.Where(sqlWhere);
            }
            else
                return _Entities;

        }

        /// <summary>
        /// Check If Inherited `ISoftDelete` Interface, Soft Delete Or Not.
        /// 检查是否继承`ISoftDelete` 接口, 是否软删除
        /// </summary>
        /// <param name="model">实体</param>
        private void CheckDelete(TEntity model)
        {
            if (IsISoftDelete())
            {
                var soft = (ISoftDelete)model;
                soft.IsDeleted = true;
                var entity = (TEntity)soft;
                _StDb.Update(entity);
            }
            else
                _StDb.Remove(model);
        }

        /// <summary>
        /// Check If Inherited `ISoftDelete` Interface, Soft Delete Or Not.
        /// 检查是否继承`ISoftDelete` 接口, 是否软删除
        /// </summary>
        /// <param name="model">实体</param>
        private void CheckDelete(IEnumerable<TEntity> model)
        {
            model.ForEach(x =>
            {
                CheckDelete(x);
            });
        }

        /// <summary>
        /// Check If Inherited `ICreationAudited<>` Interface, Add `CreatorId` And `CreatedTime`.
        /// 检查是否继承`ICreationAudited<>` 接口, 是否新增添加人Id和添加时间
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        private TEntity CheckInsert(TEntity model)
        {
            var creationAudited = model.GetType().GetInterface(typeof(ICreationAudited<>).Name);
            if (creationAudited.IsNull())
                return model;

            // 可能被恶意篡改源码 去掉应有属性值导致找寻第[0]个不存在
            var typeArguments = creationAudited?.GenericTypeArguments[0];
            var typeName = typeArguments?.FullName;

            if (typeName == typeof(Guid).FullName)
            {
                var creation = (ICreationAudited<TPrimaryKey>)model;
                // 双重校验是否为正确Guid格式
                if (typeof(TPrimaryKey) == typeof(Guid))
                {
                    creation.CreatorId = _IdentityInfo.Identity?.UId.AsTo<TPrimaryKey>();
                }
                creation.CreatedTime = DateTime.Now;
            }
            return model;
        }

        /// <summary>
        /// Check If Inherited `ICreationAudited<>` Interface, Add `CreatorId` And `CreatedTime`.
        /// 检查是否继承`ICreationAudited<>` 接口, 是否新增添加人Id和添加时间
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        private IEnumerable<TEntity> CheckInsert(IEnumerable<TEntity> model)
        {
            model.ForEach(x =>
            {
                CheckInsert(x);
            });
            return model;
        }

        /// <summary>
        /// Check If Inherited `IModificationAudited<>` Interface, Add `LastModifierId` And `LastModifierTime`.
        /// 检查是否继承`IModificationAudited<>` 接口, 是否新增修改人Id和修改时间
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        private TEntity CheckUpdate(TEntity model)
        {
            var modificationAudited = model.GetType().GetInterface(typeof(IModificationAudited<>).Name);
            if (modificationAudited.IsNull())
                return model;

            // 可能被恶意篡改源码 去掉应有属性值导致找寻第[0]个不存在
            var typeArguments = modificationAudited?.GenericTypeArguments[0];
            var typeName = typeArguments?.FullName;

            if (typeName == typeof(Guid).FullName)
            {
                var creation = (IModificationAudited<TPrimaryKey>)model;
                // 双重校验是否为正确Guid格式
                if (typeof(TPrimaryKey) == typeof(Guid))
                {
                    creation.LastModifierId = _IdentityInfo.Identity?.UId.AsTo<TPrimaryKey>();
                }
                creation.LastModifierTime = DateTime.Now;
            }
            return model;
        }

        /// <summary>
        /// Check If Inherited `IModificationAudited<>` Interface, Add `LastModifierId` And `LastModifierTime`.
        /// 检查是否继承`IModificationAudited<>` 接口, 是否新增修改人Id和修改时间
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        private IEnumerable<TEntity> CheckUpdate(IEnumerable<TEntity> model)
        {
            model.ForEach(x =>
            {
                CheckInsert(x);
            });
            return model;
        }



        #endregion



    }
}
