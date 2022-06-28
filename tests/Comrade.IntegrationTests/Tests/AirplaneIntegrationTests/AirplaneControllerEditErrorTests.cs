using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.AirplaneComponent.Contracts;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerEditErrorTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerEditErrorTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task AirplaneController_Edit_Error()
    {
        var changeCode = "Code testObject edit";
        var changeModel = "Model testObject edit";

        var airplaneId = new Guid("063f44b8-df8b-4f96-889a-75b9d67c546f");

        var testObject = new AirplaneEditDto
        {
            Id = airplaneId,
            Code = changeCode,
            PassengerQuantity = 6666
        };

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var result = await airplaneController.Edit(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(409, actualResultValue?.Code);
        }

        var repository = new AirplaneRepository(_fixture.SqlContextFixture);
        var airplane = await repository.GetById(airplaneId);
        Assert.NotEqual(6666, airplane!.PassengerQuantity);
        Assert.NotEqual(changeCode, airplane.Code);
        Assert.NotEqual(changeModel, airplane.Model);
    }
}