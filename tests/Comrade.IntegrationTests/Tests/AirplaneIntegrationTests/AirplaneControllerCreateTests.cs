using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public sealed class AirplaneControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerCreateTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public async Task AirplaneController_Create()
    {
        var testObject = new AirplaneCreateDto
        {
            Code = "444",
            Model = "585",
            PassengerQuantity = 456
        };

        var sp = _fixture.InitiateConxtext("test_database_AirplaneController_Create");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(context, mediator);

        var result = await airplaneController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }


    [Fact]
    public async Task AirplaneController_Create_Error()
    {
        var testObject = new AirplaneCreateDto
        {
            Code = "123",
            PassengerQuantity = 456
        };

        var sp = _fixture.InitiateConxtext("test_database_AirplaneController_Create_Error");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(context, mediator);

        var result = await airplaneController.Create(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }
    }
}