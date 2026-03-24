using System.Text.Json;
using StackExchange.Redis;

namespace Skinet.Api.Services;

public class ProductCacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(10);

    public ProductCacheService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var db = _redis.GetDatabase();
        var cached = await db.StringGetAsync(key);

        if (cached.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(cached!);
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var db = _redis.GetDatabase();
        var json = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, json, _cacheDuration);
    }

    public async Task RemoveAsync(string key)
    {
        var db = _redis.GetDatabase();
        await db.KeyDeleteAsync(key);
    }
}
