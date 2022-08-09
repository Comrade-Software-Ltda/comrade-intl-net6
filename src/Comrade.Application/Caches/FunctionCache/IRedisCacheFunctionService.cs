namespace Comrade.Application.Caches.FunctionCache;

public interface IRedisCacheFunctionService
{
    long? GetCacheFunction(string nameFunction, long n);
    long  SetCacheFunction(string nameFunction, long n, long valor);
    void  RemoveCacheFunction(string nameFunction, long n);
    void  RemoveAllCacheBelowOrEqualFunction(string nameFunction, long threshold);
}
