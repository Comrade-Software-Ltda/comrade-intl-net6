using Comrade.Domain.Enums;

namespace Comrade.Application.Caches.FunctionCache;

public class RedisCacheFunctionService(IRedisCacheService cacheService) : IRedisCacheFunctionService
{
    public long? GetCacheFunction(EnumFunction nameFunction, long n)
    {
        return cacheService.GetCache<long?>(FormatCacheKey(nameFunction, n));
    }

    public long SetCacheFunction(EnumFunction nameFunction, long n, long valor)
    {
        return cacheService.SetCache(FormatCacheKey(nameFunction, n), valor);
    }

    public void RemoveCacheFunction(EnumFunction nameFunction, long n)
    {
        cacheService.RemoveCache(FormatCacheKey(nameFunction, n));
    }

    public void RemoveAllCacheBelowOrEqualFunction(EnumFunction nameFunction, long threshold)
    {
        var i = threshold;
        for (; i >= 0; i--)
        {
            RemoveCacheFunction(nameFunction, i);
        }
    }

    private static string FormatCacheKey(EnumFunction nameFunction, long n)
    {
        return nameFunction + n.ToString(CultureInfo.CurrentCulture);
    }
}
