using Comrade.Application.Caches;
using Comrade.Application.Components.AlticciComponent.Queries;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class FunctionInjectionService
{
    public static AlticciQuery GetFunctionQuery(IRedisCacheService cache)
    {
        return new AlticciQuery(cache);
    }
}
