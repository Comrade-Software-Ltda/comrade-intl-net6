using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Comrade.Application.Caches;

public sealed class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T? GetCache<T>(string key)
    {
        var value = _cache.GetString(key);
        if (value != null)
        {
            return JsonSerializer.Deserialize<T>(value);
        }
        return default;
    }

    public T SetCache<T>(string key, T value)
    {
        var timeOut = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromMinutes(60)
        };
        _cache.SetString(key, JsonSerializer.Serialize(value), timeOut);
        return value;
    }

    public void removeCache(string key)
    {
        _cache.Remove(key);
    }
}
