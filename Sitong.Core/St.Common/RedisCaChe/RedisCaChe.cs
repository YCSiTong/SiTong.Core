using St.Common.Helper;
using St.Exceptions.Redis;
using St.Extensions;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace St.Common.RedisCaChe
{
    public class RedisCaChe : IRedisCaChe
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCaChe(ConnectionMultiplexer redisConnection)
        {
            _redis = redisConnection;
            _database = redisConnection.GetDatabase();// 可自行扩展选择Redis空间区域例如：redisConnection.GetDatabase(0/1/2/3/4/5...)以此类推
        }


        /// <summary>
        /// 清空所有键值
        /// </summary>
        public void Clear()
        {
            _redis.GetEndPoints().ForEach(x =>
           {
               var server = _redis.GetServer(x);
               server.Keys().ForEach(key =>
               {
                   _database.KeyDelete(key);
               });

           });
        }

        #region Key/Val

        /// <summary>
        /// 检测指定键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> IsKeyBoolAsync(string key)
            => key.IsNotEmptyOrNull() ? await _database.KeyExistsAsync(key) : throw new RedisParametersException(nameof(IsKeyBoolAsync));
        /// <summary>
        /// 删除指定键值
        /// </summary>
        /// <param name="key">键</param>
        public async Task<bool> RemoveAsync(string key)
            => key.IsNotEmptyOrNull() ? await _database.KeyDeleteAsync(key) : throw new RedisParametersException(nameof(RemoveAsync));
        /// <summary>
        /// 存储键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="timeSpan">缓存时间</param>
        /// <returns></returns>
        public async Task<bool> SetValAsync(string key, object val, TimeSpan? timeSpan = null)
        {
            if (key.IsNullOrEmpty() && val.IsNullOrEmpty()) throw new RedisParametersException(nameof(SetValAsync));
            if (timeSpan.HasValue)
                return await _database.StringSetAsync(key, SerializeHelper.Serialize(val), timeSpan.Value);
            else
                return await _database.StringSetAsync(key, SerializeHelper.Serialize(val));
        }
        /// <summary>
        /// 获取<see cref="string"/>类型的值,不存在则为Null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<string> GetValAsync(string key)
            => key.IsNotEmptyOrNull() ? await _database.StringGetAsync(key) : throw new RedisParametersException(nameof(GetValAsync));
        /// <summary>
        /// 获取<typeparamref name="TEntity"/>类型的值,不存在返回default
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TEntity> GetValAsync<TEntity>(string key)
        {
            if (key.IsNotEmptyOrNull()) throw new RedisParametersException(nameof(GetValAsync));
            var val = await _database.StringGetAsync(key);
            if (val.HasValue)
                return SerializeHelper.Deserialize<TEntity>(val);
            else
                return default;
        }

        #endregion


        #region Hash
        /// <summary>
        /// 获取Hash表<see cref="string"/>类型的值,不存在为Null
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<string> HGetValAsync(string hashName, string field)
         => (hashName.IsNotEmptyOrNull() && field.IsNotEmptyOrNull()) ? await _database.HashGetAsync(hashName, field) : throw new RedisParametersException(nameof(HGetValAsync)); 
        #endregion

    }
}
