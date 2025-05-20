namespace Nauther.Framework.Infrastructure.Caching.RedisCache;

public interface IRedisCacheService
{
    Task AddAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<T?> GetAsync<T>(string key);
    Task RemoveAsync(string key);
    
    Task AddListAsync<T>(string key, IList<T> list, TimeSpan? expiry = null);
    Task<List<T>?> GetListAsync<T>(string key);
}