namespace RedisCache.Common.Repository
{
    public interface ICacheCommonRepository
    {
        T Get<T>(string key);
        bool Set<T>(string key, T value, DateTimeOffset expires);
        object Remove(string key);
        Task<T> GetAsync<T>(string key);
        Task<object> RemoveAsync(string key);
        Task<bool> SetAsync<T>(string key, T value, DateTimeOffset expires);
        Task<bool> KeyExistsAsync(string key);
        bool KeyExists(string key);
    }
}
