using St.Common.Helper;
using St.Extensions;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace St.Common.RedisCaChe
{
    public class RedisCaChe : IRedisCaChe
    {
        private readonly string _RedisConnectionStr;
        private volatile ConnectionMultiplexer _Redis;
        private object _RedisLock = new object();

        public RedisCaChe()
        {
            _RedisConnectionStr = AppSettings.GetVal("Redis", "RedisConnection");
            _RedisConnectionStr.NotEmptyOrNull(nameof(_RedisConnectionStr));
            _Redis = GetRedisConnection();
        }
        /// <summary>
        /// 获取Redis工作单元
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            if (_Redis != null && _Redis.IsConnected) return _Redis;

            lock (_RedisLock)
            {
                if (_Redis != null) _Redis.Dispose();

                return _Redis = ConnectionMultiplexer.Connect(_RedisConnectionStr);
            }
        }

        /// <summary>
        /// 清空所有键值
        /// </summary>
        public void Clear()
        {
            foreach (var item in _Redis.GetEndPoints())
            {
                var server = _Redis.GetServer(item);
                foreach (var ddd in server.Keys())
                {
                    _Redis.GetDatabase().KeyDelete(ddd);
                }
            }
        }


        /// <summary>
        /// 获取<see cref="string"/>类型的值,不存在则为Null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string GetVal(string key)
            => _Redis.GetDatabase().StringGet(key);
        /// <summary>
        /// 获取<see cref="{TEntity}"/>类型的值,不存在返回default
        /// </summary>
        /// <typeparam name="SiTong"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TEntity GetVal<TEntity>(string key)
        {
            var val = _Redis.GetDatabase().StringGet(key);
            if (val.HasValue)
                return SerializeHelper.Deserialize<TEntity>(val);
            else
                return default;
        }
        /// <summary>
        /// 存储键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="timeSpan">缓存时间</param>
        /// <returns></returns>
        public bool SetVal(string key, object val, TimeSpan timeSpan)
            => _Redis.GetDatabase().StringSet(key, SerializeHelper.Serialize(val), timeSpan);
        /// <summary>
        /// 删除指定键值
        /// </summary>
        /// <param name="key">键</param>
        public bool Remove(string key)
            => _Redis.GetDatabase().KeyDelete(key);
        /// <summary>
        /// 检测指定键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool IsKeyBool(string key)
            => _Redis.GetDatabase().KeyExists(key);



        /// <summary>
        /// 检测指定键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> IsKeyBoolAsync(string key)
            => await _Redis.GetDatabase().KeyExistsAsync(key);
        /// <summary>
        /// 删除指定键值
        /// </summary>
        /// <param name="key">键</param>
        public async Task<bool> RemoveAsync(string key)
            => await _Redis.GetDatabase().KeyDeleteAsync(key);
        /// <summary>
        /// 存储键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="timeSpan">缓存时间</param>
        /// <returns></returns>
        public async Task<bool> SetValAsync(string key, object val, TimeSpan timeSpan)
            => await _Redis.GetDatabase().StringSetAsync(key, SerializeHelper.Serialize(val), timeSpan);
        /// <summary>
        /// 获取<see cref="string"/>类型的值,不存在则为Null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<string> GetValAsync(string key)
            => await _Redis.GetDatabase().StringGetAsync(key);
        /// <summary>
        /// 获取<see cref="{TEntity}"/>类型的值,不存在返回default
        /// </summary>
        /// <typeparam name="SiTong"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TEntity> GetValAsync<TEntity>(string key)
        {
            var val = await _Redis.GetDatabase().StringGetAsync(key);
            if (val.HasValue)
                return SerializeHelper.Deserialize<TEntity>(val);
            else
                return default;
        }
    }
}
