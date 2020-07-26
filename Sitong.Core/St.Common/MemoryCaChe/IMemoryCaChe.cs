using System;
namespace St.Common.MemoryCaChe
{
    public interface IMemoryCaChe
    {

        /// <summary>
        /// 根据Key找到缓存值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        object GetVal(object key);
        /// <summary>
        /// 根据Key找到缓存值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        TEntity GetVal<TEntity>(object key);
        /// <summary>
        /// 存取数据至内存缓存中
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expirationTime">过期时间</param>
        void SetVal(object key, object value, TimeSpan expirationTime);
        /// <summary>
        /// 根据Key删除对应缓存值
        /// </summary>
        /// <param name="key">键名</param>
        void Remove(object key);
    }
}
