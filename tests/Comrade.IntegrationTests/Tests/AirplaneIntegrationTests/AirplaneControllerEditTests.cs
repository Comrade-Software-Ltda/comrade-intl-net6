using System;
using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneComponent.Dtos;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task AirplaneController_Edit()
    {
        var changeCode = "Code testObject edit";
        var changeModel = "Model testObject edit";

        var airplaneId = new Guid("063f44b8-df8b-4f96-889a-75b9d67c546f");

        var testObject = new AirplaneEditDto
        {
            Id = airplaneId,
            Code = changeCode,
            Model = changeModel,
            PassengerQuantity = 6666
        };

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var result = await airplaneController.Edit(testObject);

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }

        var repository = new AirplaneRepository(_fixture.SqlContextFixture);
        var airplane = await repository.GetById(airplaneId);
        Assert.Equal(6666, airplane!.PassengerQuantity);
        Assert.Equal(changeCode, airplane.Code);
        Assert.Equal(changeModel, airplane.Model);
    }
}