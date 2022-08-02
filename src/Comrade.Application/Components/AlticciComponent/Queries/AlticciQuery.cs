using Comrade.Application.Caches;

namespace Comrade.Application.Components.AlticciComponent.Queries;

public class AlticciQuery : IAlticciQuery
{
    private readonly IRedisCacheService _CacheService;

    public AlticciQuery(IRedisCacheService CacheService)
    {
        _CacheService = CacheService;
    }

    private int? GetCacheAlticci(int n)
    {
        return _CacheService.GetCache<int?>(n.ToString(CultureInfo.CurrentCulture));
    }

    private int SetCacheAlticci(int n, int valor)
    {
        return _CacheService.SetCache(n.ToString(CultureInfo.CurrentCulture), valor);
    }

    public int CalculaAlticci(int n)
    {
        if (n < 0)
        {
            return 0;
        }
        int result;
        int? cache = GetCacheAlticci(n);
        if (cache is null)
        {
            result = SetCacheAlticci(n, CalculaAlticciFunc(n));
        }
        else
        {
            result = (int)cache;
        }
        return result;
    }

    private int CalculaAlticciFunc(int n)
    {
        if (n <= 0)
        {
            return 0;
        }
        if (n == 1 || n == 2)
        {
            return 1;
        }
        int result;
        int? cache = GetCacheAlticci(n);
        if (cache is null)
        {
            result = SetCacheAlticci(n, CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2));
        }
        else
        {
            result = (int)cache;
        }
        return result;
    }
}
