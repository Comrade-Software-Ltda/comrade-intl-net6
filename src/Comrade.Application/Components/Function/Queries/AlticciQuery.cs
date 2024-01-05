using Comrade.Application.Bases;
using Comrade.Application.Caches.FunctionCache;
using Comrade.Domain.Enums;

namespace Comrade.Application.Components.Function.Queries;

public class AlticciQuery(IRedisCacheFunctionService cacheService) : IAlticciQuery
{
    private const EnumFunction NameFunction = EnumFunction.Alticci;
    private FunctionReturnDto? _functionReturnDto;

    public FunctionReturnDto? CalculaAlticci(long n)
    {
        if (n < 0)
        {
            return null;
        }

        // ReSharper disable once UseObjectOrCollectionInitializer
        _functionReturnDto = new FunctionReturnDto(n);
        _functionReturnDto.ResultDto.Fn = CalculaAlticciFunc(n);
        return _functionReturnDto;
    }

    private long CalculaAlticciFunc(long n)
    {
        long result;
        var cache = cacheService.GetCacheFunction(NameFunction, n);
        if (cache is null)
        {
            // ReSharper disable once ConvertIfStatementToSwitchStatement
            if (n <= 0)
            {
                result = cacheService.SetCacheFunction(NameFunction, 0, 0);
            }
            else if (n is 1 or 2)
            {
                result = cacheService.SetCacheFunction(NameFunction, n, 1);
            }
            else
            {
                result = cacheService.SetCacheFunction(NameFunction, n,
                    CalculaAlticciFunc(n - 3) + CalculaAlticciFunc(n - 2));
            }

            _functionReturnDto?.DoCache.Add(new FunctionDto(n, result)); // For tests only
        }
        else
        {
            result = (long) cache;
            _functionReturnDto?.UseCache.Add(new FunctionDto(n, result)); // For tests only
        }

        return result;
    }
}
