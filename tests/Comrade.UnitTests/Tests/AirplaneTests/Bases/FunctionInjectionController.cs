using Comrade.Api.Controllers.V1.FunctionApi;
using Comrade.Application.Caches.FunctionCache;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class FunctionInjectionController
{
    public static FunctionController GetFunctionController(IRedisCacheFunctionService cache)
    {
        return new FunctionController(FunctionInjectionService.GetFunctionQuery(cache));
    }
}
