using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    readonly ServiceProviderFixture _fixture;

    public AirplaneControllerDeleteTests(ServiceProviderFixture fixture)
    {
        this._fixture = fixture;
    }

    [Fact]
    public async Task AirplaneController_Delete()
    {
        var sp = _fixture.InitiateConxtext("test_database_AirplaneController_Delete");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var idAirplane = 1;

        var airplaneController = AirplaneInjectionController.GetAirplaneController(context, mediator);
        _ = await airplaneController.Delete(idAirplane);

        var repository = new AirplaneRepository(context);
        var airplane = await repository.GetById(1);
        Assert.Null(airplane);
    }
}