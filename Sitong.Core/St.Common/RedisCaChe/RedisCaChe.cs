using St.Common.Helper;
using St.Exceptions.Redis;
using St.Extensions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
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

        #region Distributed Lock
        /// <summary>
        /// 开启Redis分布式锁
        /// </summary>
        /// <param name="key">锁名</param>
        /// <param name="expirationTimeSeconds">自动释放时间，默认5分钟</param>
        /// <returns></returns>
        public async Task BeginLock(string key, int expirationTimeSeconds = 5)
        {
            while (true)
            {
                var lockResult = await _database.LockTakeAsync(key, Task.CurrentId, TimeSpan.FromSeconds(expirationTimeSeconds));
                if (lockResult)
                    break;
            }
        }
        /// <summary>
        /// 解除Redis分布式锁
        /// </summary>
        /// <param name="key">锁名</param>
        /// <returns></returns>
        public async Task<bool> UnLock(string key)
        {
            return await _database.LockReleaseAsync(key, Task.CurrentId);
        }
        #endregion

        #region Key/Val

        /// <summary>
        /// 检测指定键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(string key)
            => key.IsNotEmptyOrNull() ? await _database.KeyExistsAsync(key) : throw new RedisParametersException(nameof(IsExistAsync));
        /// <summary>
        /// 删除指定键值
        /// </summary>
        /// <param name="key">键</param>
        public async Task<bool> DelAsync(string key)
            => key.IsNotEmptyOrNull() ? await _database.KeyDeleteAsync(key) : throw new RedisParametersException(nameof(DelAsync));
        /// <summary>
        /// 存储键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="timeSpan">缓存时间</param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string key, object val, TimeSpan? timeSpan = null)
        {
            if (key.IsNullOrEmpty() && val.IsNullOrEmpty()) throw new RedisParametersException(nameof(SetAsync));
            return timeSpan.HasValue ?
                await _database.StringSetAsync(key, SerializeHelper.Serialize(val), timeSpan.Value) :
                await _database.StringSetAsync(key, SerializeHelper.Serialize(val));
        }
        /// <summary>
        /// 获取<see cref="string"/>类型的值,不存在则为Null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            if (key.IsNotEmptyOrNull())
            {
                var result = await _database.StringGetAsync(key);
                return result.HasValue ? result : default;
            }
            throw new RedisParametersException(nameof(GetAsync));
        }
        /// <summary>
        /// 获取<typeparamref name="TEntity"/>类型的值,不存在返回default
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync<TEntity>(string key)
        {
            if (key.IsNotEmptyOrNull()) throw new RedisParametersException(nameof(GetAsync));
            var val = await _database.StringGetAsync(key);
            return val.HasValue ? SerializeHelper.Deserialize<TEntity>(val) : default;
        }
        /// <summary>
        /// 模糊查询符合条件的键值对,不存在则为Null
        /// </summary>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public async Task<string[]> GetLikeAsync(string key)
        {
            if (key.IsNotEmptyOrNull())
            {
                var result = await _database.ScriptEvaluateAsync(LuaScript.Prepare("return redis.call('keys',@paramter)"), new { paramter = key });
                return (!result.IsNull) ? (string[])result : default;
            }
            throw new RedisParametersException(nameof(GetLikeAsync));
        }

        #endregion


        #region Hash

        /// <summary>
        /// 获取Hash表<see cref="string"/>类型的值,不存在为Null
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<string> HGetAsync(string hashName, string field)
        {
            if (hashName.IsNotEmptyOrNull() && field.IsNotEmptyOrNull())
            {
                var result = await _database.HashGetAsync(hashName, field);
                return result.HasValue ? result : default;
            }
            throw new RedisParametersException(nameof(HGetAsync));
        }
        /// <summary>
        /// 获取Hash表<typeparamref name="TEntity"/>类型的值,不存在为Null
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<TEntity> HGetAsync<TEntity>(string hashName, string field)
        {
            if (hashName.IsNotEmptyOrNull() && field.IsNotEmptyOrNull())
            {
                var result = await _database.HashGetAsync(hashName, field);
                return result.HasValue ? SerializeHelper.Deserialize<TEntity>(result) : default;
            }
            throw new RedisParametersException(nameof(HGetAsync));
        }
        /// <summary>
        /// 获取Hash表<typeparamref name="TEntity"/>类型的值,不存在为Null
        /// </summary>
        /// <typeparam name="TEntity">类型</typeparam>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<List<TEntity>> HGetAsync<TEntity>(string hashName, IEnumerable<string> field)
        {
            if (hashName.IsNotEmptyOrNull() && field.IsNotNull())
            {
                List<TEntity> list = new List<TEntity>();
                foreach (var item in field)
                {
                    var result = await _database.HashGetAsync(hashName, item);
                    if (!result.HasValue) continue;

                    list.Add(SerializeHelper.Deserialize<TEntity>(result));
                }
                return list;
            }
            throw new RedisParametersException(nameof(HGetAsync));
        }
        /// <summary>
        /// 获取Hash表所有键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> HGetAsync(string hashName)
        {
            if (hashName.IsNotEmptyOrNull())
            {
                Dictionary<string, object> keyValues = new Dictionary<string, object>();
                var result = await _database.HashGetAllAsync(hashName);
                foreach (var item in result)
                {
                    keyValues.Add(item.Name, item.Value);
                }
                return keyValues;
            }
            throw new RedisParametersException(nameof(HGetAsync));
        }
        /// <summary>
        /// 获取Hash表所有键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <returns></returns>
        public async Task<Dictionary<string, TEntity>> HGetAsync<TEntity>(string hashName)
        {
            if (hashName.IsNotEmptyOrNull())
            {
                Dictionary<string, TEntity> keyValues = new Dictionary<string, TEntity>();
                var result = await _database.HashGetAllAsync(hashName);
                foreach (var item in result)
                {
                    keyValues.Add(item.Name, item.Value.HasValue ? SerializeHelper.Deserialize<TEntity>(item.Value) : default);
                }
                return keyValues;
            }
            throw new RedisParametersException(nameof(HGetAsync));
        }

        /// <summary>
        /// 删除Hash表指定键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<bool> HDelAsync(string hashName, string field)
            => (hashName.IsNotEmptyOrNull() && field.IsNotEmptyOrNull()) ? await _database.HashDeleteAsync(hashName, field) : throw new RedisParametersException(nameof(HDelAsync));
        /// <summary>
        /// 检测指定Hash表键值是否存在
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<bool> HIsExistAsync(string hashName, string field)
            => (hashName.IsNotEmptyOrNull() && field.IsNotEmptyOrNull()) ? await _database.HashExistsAsync(hashName, field) : throw new RedisParametersException(nameof(HIsExistAsync));
        /// <summary>
        /// 检测指定Hash表键值是否存在
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <returns></returns>
        public async Task<bool> HIsExistAsync(string hashName, IEnumerable<string> field)
        {
            if (hashName.IsNotEmptyOrNull() && field.IsNotNull())
            {
                foreach (var item in field)
                {
                    var result = await HIsExistAsync(hashName, item);
                    if (!result)
                        return false;
                }
            }
            throw new RedisParametersException(nameof(HIsExistAsync));
        }
        /// <summary>
        /// 存储Hash表键值
        /// </summary>
        /// <param name="hashName">Hash名</param>
        /// <param name="field">Hash内键名</param>
        /// <param name="val">值</param>
        /// <returns></returns>
        public async Task<bool> HSetAsync(string hashName, string field, object val)
            => (hashName.IsNotEmptyOrNull() && field.IsNotEmptyOrNull() && val.IsNotEmptyOrNull()) ? await _database.HashSetAsync(hashName, field, SerializeHelper.Serialize(val)) : throw new RedisParametersException(nameof(HSetAsync));

        #endregion

    }
}
