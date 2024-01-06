using StackExchange.Redis;
using System.Text.Json;

namespace RedisCache.Common.Repository
{
    public class CacheCommonRepository : ICacheCommonRepository
    {
        private readonly IDatabase _database;
        public CacheCommonRepository(ConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        /// <summary>
        /// Performs synchronous get operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>An object of the type argument passed in to the method</returns>
        public T Get<T>(string key)
        {
            var value = _database.StringGet(key);
            if (!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);

            return default(T);
        }

        /// <summary>
        /// Performs asynchronous get operations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>An object of the type argument passed in to the method</returns>
        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<T>(value);

            return default(T);
        }

        /// <summary>
        /// Performs synchronous delete operation
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Remove(string key)
        {
            var exists = _database.KeyExists(key);
            if (exists)
                return _database.KeyDelete(key);

            return false;
        }

        /// <summary>
        /// Performs asynchronous delete operation
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<object> RemoveAsync(string key)
        {
            var exists = await _database.KeyExistsAsync(key);
            if (exists)
                return await _database.KeyDeleteAsync(key);

            return false;
        }

        /// <summary>
        /// Performs synchronous set operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <returns>True or False, for success or failure respectively</returns>
        public bool Set<T>(string key, T value, DateTimeOffset expires)
        {
            var expiresAt = expires.DateTime.Subtract(DateTime.UtcNow);
            return _database.StringSet(key, JsonSerializer.Serialize(value), expiresAt);
        }

        /// <summary>
        /// Performs asynchronous set operation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <returns>True of False, for success or failure respectively</returns>
        public async Task<bool> SetAsync<T>(string key, T value, DateTimeOffset expires)
        {
            var expiresAt = expires.DateTime.Subtract(DateTime.UtcNow);
            return await _database.StringSetAsync(key, JsonSerializer.Serialize(value), expiresAt);
        }

        /// <summary>
        /// Checks if a key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns>True if key exists, false otherwise</returns>
        public async Task<bool> KeyExistsAsync(string key) =>
            await _database.KeyExistsAsync(key);

        /// <summary>
        /// Checks if a key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns>True if key exists, false otherwise</returns>
        public bool KeyExists(string key) =>
            _database.KeyExists(key);
    }
}
