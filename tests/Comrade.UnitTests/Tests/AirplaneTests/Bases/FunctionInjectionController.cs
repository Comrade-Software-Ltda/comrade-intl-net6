using Comrade.Api.Controllers.V1.AlticciApi;
using Comrade.Application.Caches;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class FunctionInjectionController
{
    public static FunctionController GetFunctionController(IRedisCacheService cache)
    {
        var functionQuery = FunctionInjectionService.GetFunctionQuery(cache);
        return new FunctionController(functionQuery);
    }
}
