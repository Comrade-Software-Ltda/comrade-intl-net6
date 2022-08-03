using Comrade.Application.Caches;

namespace Comrade.Application.Components.AlticciComponent.Queries;

public class AlticciQuery : IAlticciQuery
{
    private readonly IRedisCacheService _CacheService;

    public AlticciQuery(IRedisCacheService CacheService)
    {
        _CacheService = CacheService;
    }

    private long? GetCacheAlticci(long n)
    {
        return _CacheService.GetCache<long?>(n.ToString(CultureInfo.CurrentCulture));
    }

    private long SetCacheAlticci(long n, long valor)
    {
        return _CacheService.SetCache(n.ToString(CultureInfo.CurrentCulture), valor);
    }

    public long CalculaAlticci(long n)
    {
        if (n < 0)
        {
            return 0;
        }
        long result;
        long? cache = GetCacheAlticci(n);
        if (cache is null)
        {
            result = SetCacheAlticci(n, CalculaAlticciFunc(n));
        }
        else
        {
            result = (long)cache;
        }
        return result;
    }

    private long CalculaAlticciFunc(long n)
    {
        if (n <= 0)
        {
            return 0;
        }
        if (n == 1 || n == 2)
        {
            return 1;
        }
        long result;
        long? cache = GetCacheAlticci(n);
        if (cache is null)
        {
            result = SetCacheAlticci(n, CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2));
        }
        else
        {
            result = (long)cache;
        }
        return result;
    }
}
