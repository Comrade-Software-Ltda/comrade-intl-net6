using Comrade.Application.Caches.FunctionCache;

namespace Comrade.Application.Components.FunctionComponent.Queries;

public class AlticciQuery : IAlticciQuery
{
    private readonly IRedisCacheFunctionService _cacheService;
    private readonly string _nameFunction = "Alticci";

    public AlticciQuery(IRedisCacheFunctionService cacheService)
    {
        _cacheService = cacheService;
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
        long? cache = _cacheService.GetCacheFunction(_nameFunction, n);
        if (cache is null)
        {
            if (n <= 0)
            {
                result = _cacheService.SetCacheFunction(_nameFunction, 0, 0);
            }
            else if (n == 1 || n == 2)
            {
                result = _cacheService.SetCacheFunction(_nameFunction, n, 1);
            }
            else
            {
                result = _cacheService.SetCacheFunction(_nameFunction, n, CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2));
            }
        }
        else
        {
            result = (long)cache;
        }
        return result;
    }
}
