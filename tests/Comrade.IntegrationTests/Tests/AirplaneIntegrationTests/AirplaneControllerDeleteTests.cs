using System;
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
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task AirplaneController_Delete()
    {
        var airplaneId = new Guid("063f44b8-df8b-4f96-889a-75b9d67c546f");

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        _ = await airplaneController.Delete(airplaneId);

        var repository = new AirplaneRepository(_fixture.SqlContextFixture);
        var airplane = await repository.GetById(airplaneId);
        Assert.Null(airplane);
    }
}
