using Comrade.Domain.Enums;

namespace Comrade.Application.Caches.FunctionCache;

public interface IRedisCacheFunctionService
{
    long? GetCacheFunction(EnumFunction nameFunction, long n);
    long  SetCacheFunction(EnumFunction nameFunction, long n, long valor);
    void  RemoveCacheFunction(EnumFunction nameFunction, long n);
    void  RemoveAllCacheBelowOrEqualFunction(EnumFunction nameFunction, long threshold);
}
