using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Common.RedisCaChe
{
    public interface IRedisCaChe
    {

        #region Distributed Lock
        /// <summary>
        /// 开启Redis分布式锁
        /// </summary>
        /// <param name="key">锁名</param>
        /// <param name="expirationTimeSeconds">自动释放时间，默认5分钟</param>
        /// <returns></returns>
        Task BeginLock(string key, int expirationTimeSeconds = 5);
        /// <summary>
        /// 解除Redis分布式锁
        /// </summary>
        /// <param name="key">锁名</param>
        /// <returns></returns>
        Task<bool> UnLock(string key);
        #endregion

        /// <summary>
        /// 清空所有值
        /// </summary>
        void Clear();
        /// <summary>
        /// 设置连接Redis的库
        /// </summary>
        /// <param name="Db">数据库</param>
        void SetDbConnection(int Db = 0);

        #region Auto-Increment
        /// <summary>
        /// 自动增加
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="addNumber">数值</param>
        /// <returns></returns>
        Task<double> AutoIncrement(string key, double addNumber = 1);
        /// <summary>
        /// 自动减少
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="SubtractNumber">数值</param>
        /// <returns></returns>
        Task<double> AutoDecrement(string key, double SubtractNumber = 1);
        #endregion

        #region Key/Val

        /// <summary>
        /// 获取<see cref="string"/>类型的值,不存在则为Null
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        Task<string> GetAsync(string key);
        /// <summary>
        /// 获取<typeparamref name="TEntity"/>类型的值,不存在返回default
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="key">键名</param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(string key);
        /// <summary>
        /// 模糊查询符合条件的键值对,不存在则为Null
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        Task<string[]> GetLikeAsync(string key);
        /// <summary>
        /// 存储键值
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="val">值</param>
        /// <param name="timeSpan">缓存时间</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, object val, TimeSpan? timeSpan);
        /// <summary>
        /// 检测指定键值是否存在
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        Task<bool> IsExistAsync(string key);
        /// <summary>
        /// 删除指定键值
        /// </summary>
        /// <param name="key">键名</param>
        Task<bool> DelAsync(string key);

        #endregion

        #region Hash
        /// <summary>
        /// 获取Hash表<see cref="string"/>类型的值,不存在为Null
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<string> HGetAsync(string hashName, string field);
        /// <summary>
        /// 获取Hash表<typeparamref name="TEntity"/>类型的值,不存在为Null
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<TEntity> HGetAsync<TEntity>(string hashName, string field);
        /// <summary>
        /// 获取Hash表<typeparamref name="TEntity"/>类型的值,不存在为Null
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<List<TEntity>> HGetAsync<TEntity>(string hashName, IEnumerable<string> field);
        /// <summary>
        /// 获取Hash表所有键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <returns></returns>
        Task<Dictionary<string, object>> HGetAsync(string hashName);
        /// <summary>
        /// 获取Hash表所有键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <returns></returns>
        Task<Dictionary<string, TEntity>> HGetAsync<TEntity>(string hashName);
        /// <summary>
        /// 删除Hash表指定键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<bool> HDelAsync(string hashName, string field);
        /// <summary>
        /// 检测指定Hash表键值是否存在
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<bool> HIsExistAsync(string hashName, string field);
        /// <summary>
        /// 检测指定Hash表键值是否存在
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<bool> HIsExistAsync(string hashName, IEnumerable<string> field);
        /// <summary>
        /// 存储Hash表键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        Task<bool> HSetAsync(string hashName, string field, object val);
        #endregion

    }
}
