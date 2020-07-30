using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace St.DoMain.Interfaces
{
    /// <summary>
    /// 泛型仓储，实现泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {

        /// <summary>
        /// 工作单元
        /// </summary>
        IUnitOfWork UnitOfWork { get; }


        #region IQueryable
        /// <summary>
        /// 不追踪实体获取<see cref="IQueryable"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        IQueryable<TEntity> AsNoTracking();

        /// <summary>
        /// 追踪实体获取<see cref="IQueryable"/>
        /// </summary>
        /// <returns><see cref="IQueryable"/>延迟加载</returns>
        IQueryable<TEntity> AsTracking();

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
        /// <param name="id">主键</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> DeleteByIDAsync(object id);
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> DeleteAsync(TEntity model);
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
        Task<bool> UpdataAsync(TEntity model);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        Task<bool> UpdataAsync(IEnumerable<TEntity> model);
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
        /// <param name="id">主键</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool DeleteByID(object id);
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Delete(TEntity model);
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
        bool Updata(TEntity model);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="model">需要修改的数据</param>
        /// <returns><see cref="bool"/>是否成功</returns>
        bool Updata(IEnumerable<TEntity> model);
        #endregion

        #endregion



    }
}
