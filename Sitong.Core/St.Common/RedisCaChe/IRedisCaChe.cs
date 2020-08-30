using System;
using System.Threading.Tasks;

namespace St.Common.RedisCaChe
{
    public interface IRedisCaChe
    {
        /// <summary>
        /// 清空所有值
        /// </summary>
        void Clear();


        #region Key/Val

        /// <summary>
        /// 获取<see cref="string"/>类型的值,不存在则为Null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<string> GetValAsync(string key);
        /// <summary>
        /// 获取<typeparamref name="TEntity"/>类型的值,不存在返回default
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> GetValAsync<TEntity>(string key);
        /// <summary>
        /// 存储键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="timeSpan">缓存时间</param>
        /// <returns></returns>
        Task<bool> SetValAsync(string key, object val, TimeSpan? timeSpan);
        /// <summary>
        /// 检测指定键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<bool> IsKeyBoolAsync(string key);
        /// <summary>
        /// 删除指定键值
        /// </summary>
        /// <param name="key">键</param>
        Task<bool> RemoveAsync(string key);

        #endregion

        #region Hash
        /// <summary>
        /// 获取Hash表<see cref="string"/>类型的值,不存在为Null
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        Task<string> HGetValAsync(string hashName, string field);
        #endregion

    }
}
