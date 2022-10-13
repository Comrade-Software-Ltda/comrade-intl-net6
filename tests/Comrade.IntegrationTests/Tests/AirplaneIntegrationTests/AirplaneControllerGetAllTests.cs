using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerGetAllTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerGetAllTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task AirplaneController_GetAll()
    {
        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var result = await airplaneController.GetAll(null);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as PageResultDto<AirplaneDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(3, actualResultValue?.Data?.Count);
        }
    }
}
