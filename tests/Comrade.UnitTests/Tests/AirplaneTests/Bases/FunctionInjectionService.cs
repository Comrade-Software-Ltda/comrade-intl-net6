using Comrade.Application.Caches.FunctionCache;
using Comrade.Application.Components.FunctionComponent.Queries;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class FunctionInjectionService
{
    public static AlticciQuery GetFunctionQuery(IRedisCacheFunctionService cache)
    {
        return new AlticciQuery(cache);
    }
}
