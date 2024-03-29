﻿using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Comrade.Application.Caches;

public sealed class RedisCacheService(IDistributedCache cache) : IRedisCacheService
{
    public T? GetCache<T>(string key)
    {
        var value = cache.GetString(key);
        return value is not null ? JsonSerializer.Deserialize<T>(value) : default;
    }

    public T SetCache<T>(string key, T value)
    {
        var timeOut = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
            SlidingExpiration = TimeSpan.FromMinutes(60)
        };
        cache.SetString(key, JsonSerializer.Serialize(value), timeOut);
        return value;
    }

    public void RemoveCache(string key)
    {
        cache.Remove(key);
    }
}
