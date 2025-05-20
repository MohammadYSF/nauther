using Newtonsoft.Json;
using StackExchange.Redis;

namespace Nauther.Framework.Infrastructure.Caching.RedisCache;

public class RedisCacheService : IRedisCacheService
{
    #region Dependency Injection

    private readonly IDatabase _database;

    public RedisCacheService(string connectionString)
    {
        var redis = ConnectionMultiplexer.Connect(connectionString);
        _database = redis.GetDatabase();
    }

    #endregion

    public async Task AddAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var serializedValue = JsonConvert.SerializeObject(value);

        await _database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var serializedValue = await _database.StringGetAsync(key);
        if (serializedValue.IsNullOrEmpty)
        { 
            return default;
        }

        return JsonConvert.DeserializeObject<T>(serializedValue);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task AddListAsync<T>(string key, IList<T> list, TimeSpan? expiry = null)
    {
        var serializedValue = JsonConvert.SerializeObject(list);
        await _database.StringSetAsync(key, serializedValue, expiry);
    }

    public async Task<List<T>?> GetListAsync<T>(string key)
    {
        var listItems = await _database.StringGetAsync(key);
        if (listItems.IsNullOrEmpty)
            return null;

        var result = JsonConvert.DeserializeObject<List<T>>(listItems);
        return result;
    }
}