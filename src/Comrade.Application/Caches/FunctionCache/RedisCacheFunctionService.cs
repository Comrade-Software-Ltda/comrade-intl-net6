using Comrade.Domain.Enums;

namespace Comrade.Application.Caches.FunctionCache;

public class RedisCacheFunctionService : IRedisCacheFunctionService
{
    private readonly IRedisCacheService _cacheService;

    public RedisCacheFunctionService(IRedisCacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public long? GetCacheFunction(EnumFunction nameFunction, long n)
    {
        return _cacheService.GetCache<long?>(nameFunction + n.ToString(CultureInfo.CurrentCulture));
    }

    public long SetCacheFunction(EnumFunction nameFunction, long n, long valor)
    {
        return _cacheService.SetCache(nameFunction + n.ToString(CultureInfo.CurrentCulture), valor);
    }

    public void RemoveCacheFunction(EnumFunction nameFunction, long n)
    {
        _cacheService.RemoveCache(nameFunction + n.ToString(CultureInfo.CurrentCulture));
    }

    public void RemoveAllCacheBelowOrEqualFunction(EnumFunction nameFunction, long threshold)
    {
        var i = threshold;
        for (; i >= 0; i--)
        {
            RemoveCacheFunction(nameFunction, i);
        }
    }
}
