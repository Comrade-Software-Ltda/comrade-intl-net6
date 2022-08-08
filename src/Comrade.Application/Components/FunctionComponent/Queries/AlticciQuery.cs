using Comrade.Application.Caches;

namespace Comrade.Application.Components.AlticciComponent.Queries;

public class AlticciQuery : IAlticciQuery
{
    private readonly IRedisCacheService _CacheService;

    public AlticciQuery(IRedisCacheService CacheService) => _CacheService = CacheService;

    private long? GetCacheAlticci(long n)
    {
        return _CacheService.GetCache<long?>("Alticci" + n.ToString(CultureInfo.CurrentCulture));
    }

    private long SetCacheAlticci(long n, long valor)
    {
        return _CacheService.SetCache("Alticci" + n.ToString(CultureInfo.CurrentCulture), valor);
    }

    public long CalculaAlticci(long n)
    {
        if (n < 0)
        {
            return -1;
        }
        return CalculaAlticciFunc(n);
    }

    private long CalculaAlticciFunc(long n)
    {
        long result;
        long? cache = GetCacheAlticci(n);
        if (cache is null)
        {
            if (n <= 0)
            {
                result = SetCacheAlticci(0, 0);
            }
            else if (n == 1 || n == 2)
            {
                result = SetCacheAlticci(n, 1);
            }
            else
            {
                result = SetCacheAlticci(n, CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2));
            }
        }
        else
        {
            result = (long)cache;
        }
        return result;
    }
}
