using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerGetTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerGetTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AirplaneController_Get()
    {
        var sp = _fixture.InitiateConxtext("test_database_AirplaneController_Get");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var airplaneId = 1;
        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(context, mediator);
        var result = await airplaneController.GetById(airplaneId);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<AirplaneDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
        }
    }
}