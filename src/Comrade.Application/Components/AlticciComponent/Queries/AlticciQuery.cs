using Comrade.Application.Bases;
using Comrade.Application.Caches;

namespace Comrade.Application.Components.AlticciComponent.Queries;

public class AlticciQuery : IAlticciQuery
{
    private readonly IRedisCacheService _CacheService;

    public AlticciQuery(IRedisCacheService CacheService)
    {
        _CacheService = CacheService;
    }

    public AlticciDto CalculaAlticci(int n)
    {
        var result = _CacheService.GetCache<AlticciDto>(n.ToString(CultureInfo.CurrentCulture));
        if (result is null || result is not AlticciDto)
        {
            result = _CacheService.SetCache(n.ToString(CultureInfo.CurrentCulture), new AlticciDto(n, CalculaAlticciFunc(n)));
        }
        return result;
    }

    private int? CalculaAlticciFunc(int n)
    {
        if (n <= 0)
        {
            return 0;
        }
        if (n == 1 || n == 2)
        {
            return 1;
        }
        var result = _CacheService.GetCache<AlticciDto>(n.ToString(CultureInfo.CurrentCulture));
        if (result is null || result is not AlticciDto)
        {
            return CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2);
        }
        return result.AlticciAn;
    }
}
