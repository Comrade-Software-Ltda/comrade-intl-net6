using Comrade.Application.Caches.FunctionCache;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.FunctionIntegrationTests;

public class FunctionControllerTest : IClassFixture<ServiceProviderFixture>
{
    private readonly IRedisCacheFunctionService _redisCacheFunctionService;

    public FunctionControllerTest(ServiceProviderFixture fixture)
    {
        _redisCacheFunctionService = fixture.RedisCacheFunctionService;
    }

    [Fact]
    public void FunctionController_Alticci()
    {
        var functionController = FunctionInjectionController.GetFunctionController(_redisCacheFunctionService);
        long n = 10;
        _redisCacheFunctionService.RemoveAllCacheBelowOrEqualFunction("Alticci", n);
        var result = functionController.Alticci(n);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value;
            Assert.NotNull(actualResultValue);
            Assert.Equal((long)9, actualResultValue);
        }
    }
}
