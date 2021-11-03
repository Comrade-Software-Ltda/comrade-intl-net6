using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerDeleteTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.PostgresContextFixture);
    }

    [Fact]
    public async Task AirplaneController_Delete()
    {
        var idAirplane = 1;

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.PostgresContextFixture, _fixture.Mediator);
        _ = await airplaneController.Delete(idAirplane);

        var repository = new AirplaneRepository(_fixture.PostgresContextFixture);
        var airplane = await repository.GetById(1);
        Assert.Null(airplane);
    }
}