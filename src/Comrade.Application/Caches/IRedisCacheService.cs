namespace Comrade.Application.Caches;

public interface IRedisCacheService
{
    T? GetCache<T>(string key);
    T SetCache<T>(string key, T value);
    void RemoveCache(string key);
}
