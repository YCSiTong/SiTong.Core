using Microsoft.Extensions.Caching.Memory;
using System;

namespace St.Common.MemoryCaChe
{
    public class MemoryCaChe : IMemoryCaChe
    {
        private readonly IMemoryCache _MemoryCache;// Microsoft.Extensions.Caching.Memory

        public MemoryCaChe(IMemoryCache memoryCache)
        {
            _MemoryCache = memoryCache;
        }

        /// <summary>
        /// 根据Key获取泛型值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public TEntity GetVal<TEntity>(object key) => _MemoryCache.Get<TEntity>(key);
        /// <summary>
        /// 根据Key找到缓存值
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public object GetVal(object key) => _MemoryCache.Get(key);

        /// <summary>
        /// 存取数据至内存缓存中
        /// </summary>
        /// <param name="key">键名</param>
        /// <param name="value">值</param>
        /// <param name="expirationTime">过期时间</param>
        public void SetVal(object key, object value, TimeSpan expirationTime) => _MemoryCache.Set(key, value, expirationTime);

        /// <summary>
        /// 根据Key删除对应缓存值
        /// </summary>
        /// <param name="key">键名</param>
        public void Remove(object key) => _MemoryCache.Remove(key);

    }
}
