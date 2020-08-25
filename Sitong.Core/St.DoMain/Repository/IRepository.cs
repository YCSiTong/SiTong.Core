using St.DoMain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace St.DoMain.Repository
{
    /// <summary>
    /// 泛型仓储，实现泛型仓储接口
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IAsyncDisposable, IDisposable
        where TEntity : class
        where TPrimaryKey : struct
    {

        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }


        #region Query
        /// <summary>
        /// 不追踪实体获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        IQueryable<TEntity> AsNoTracking();

        /// <summary>
        /// 追踪实体获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        IQueryable<TEntity> AsTracking();
        /// <summary>
        /// 根据主键<typeparamref name="TPrimaryKey"/>获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        TEntity GetById(TPrimaryKey key);
        /// <summary>
        /// 根据主键<typeparamref name="TPrimaryKey"/>异步获取<typeparamref name="TEntity"/>
        /// </summary>
        /// <param name="key">主键值</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TPrimaryKey key);

        #endregion

        #region 异步

        #region Insert
        /// <summary>
        /// 新增单条数据
        /// </summary>
        /// <param name="model">实体类</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> InsertAsync(TEntity model);
        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> InsertAsync(IEnumerable<TEntity> model);
        #endregion

        #region Delete
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> DeleteAsync(TEntity model);
        /// <summary>
        /// 根据主键异步删除单条数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TPrimaryKey Id);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> DeleteAsync(IEnumerable<TEntity> model);
        #endregion

        #region Update
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> UpdateAsync(TEntity model);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> UpdateAsync(IEnumerable<TEntity> model);
        #endregion

        #endregion

        #region 同步

        #region Insert
        /// <summary>
        /// 新增单条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Insert(TEntity model);
        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Insert(IEnumerable<TEntity> model);
        #endregion

        #region Delete
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Delete(TEntity model);
        /// <summary>
        /// 根据主键删除单条数据
        /// </summary>
        /// <param name="Id">主键</param>
        /// <returns></returns>
        bool Delete(TPrimaryKey Id);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Delete(IEnumerable<TEntity> model);
        #endregion

        #region Update
        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Update(TEntity model);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Update(IEnumerable<TEntity> model);
        #endregion

        #endregion



    }
}
