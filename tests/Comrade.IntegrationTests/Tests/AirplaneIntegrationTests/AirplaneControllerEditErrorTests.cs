﻿using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
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
        InjectDataOnContextBase.InitializeDbForTests(_fixture.PostgresContextFixture);
    }

    [Fact]
    public async Task AirplaneController_Edit_Error()
    {
        var changeCode = "Code testObject edit";
        var changeModel = "Model testObject edit";

        var testObject = new AirplaneEditDto
        {
            Id = 1,
            Code = changeCode,
            PassengerQuantity = 6666
        };

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(_fixture.PostgresContextFixture, _fixture.Mediator);
        var result = await airplaneController.Edit(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }

        var repository = new AirplaneRepository(_fixture.PostgresContextFixture);
        var airplane = await repository.GetById(1);
        Assert.NotEqual(6666, airplane!.PassengerQuantity);
        Assert.NotEqual(changeCode, airplane.Code);
        Assert.NotEqual(changeModel, airplane.Model);
    }
}