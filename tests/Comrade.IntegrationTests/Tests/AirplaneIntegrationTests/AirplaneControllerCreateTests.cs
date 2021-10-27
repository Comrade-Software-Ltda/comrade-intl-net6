using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public sealed class AirplaneControllerCreateTests
{
    [Fact]
    public async Task AirplaneController_Create()
    {
        var testObject = new AirplaneCreateDto
        {
            Code = "444",
            Model = "585",
            PassengerQuantity = 456
        };

        var airplaneController = AirplaneInjectionController.GetAirplaneController();
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

        var airplaneController = AirplaneInjectionController.GetAirplaneController();
        var result = await airplaneController.Create(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }
    }

    [Fact]
    public async Task UcAirplaneCreate_Test_Exception()
    {
        var airplaneController = AirplaneInjectionController.GetAirplaneController();
        var result = await airplaneController.Create(new AirplaneCreateDto());

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(500, actualResultValue?.Code);
        }
    }
}