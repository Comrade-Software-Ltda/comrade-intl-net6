using System.Text.Json;
using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;
using Xunit.Abstractions;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public sealed class AirplaneControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;
    private readonly ITestOutputHelper _output;


    public AirplaneControllerCreateTests(ServiceProviderFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
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

        var oto = JsonSerializer.Serialize(testObject);

        _output.WriteLine(oto);
        _output.WriteLine(testObject.ToString());

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

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

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);

        var result = await airplaneController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }
    }
}
