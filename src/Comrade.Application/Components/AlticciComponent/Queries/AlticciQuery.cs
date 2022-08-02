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

    private int GetCacheIfExistElseSetCache(int n, int valor)
    {
        int result;
        int? cache = GetCacheAlticci(n);
        if (cache is null)
        {
            result = SetCacheAlticci(n, valor);
        }
        else
        {
            result = (int)cache;
        }
        return result;
    }

    public int CalculaAlticci(int n)
    {
        if (n < 0)
        {
            return 0;
        }
        return GetCacheIfExistElseSetCache(n, CalculaAlticciFunc(n));
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
        return GetCacheIfExistElseSetCache(n, CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2));
    }
}
