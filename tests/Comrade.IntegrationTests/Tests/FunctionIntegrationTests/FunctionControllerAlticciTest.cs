using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.FunctionIntegrationTests;

public class FunctionControllerAlticciTest : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public FunctionControllerAlticciTest(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void FunctionController_Alticci()
    {
        var functionController = FunctionInjectionController.GetFunctionController(_fixture.RedisCacheService);
        var result = functionController.Alticci(10);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value;
            Assert.NotNull(actualResultValue);
            Assert.Equal((long)9, actualResultValue);
        }
    }
}
