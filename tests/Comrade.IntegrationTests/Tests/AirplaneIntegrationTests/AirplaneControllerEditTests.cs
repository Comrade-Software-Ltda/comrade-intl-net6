using Comrade.Application.Bases;
using Comrade.Application.Services.AirplaneServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AirplaneController_Edit()
    {
        var sp = _fixture.InitiateConxtext("test_database_AirplaneController_Edit");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var changeCode = "Code testObject edit";
        var changeModel = "Model testObject edit";

        var testObject = new AirplaneEditDto
        {
            Id = 1,
            Code = changeCode,
            Model = changeModel,
            PassengerQuantity = 6666
        };

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(context, mediator);
        var result = await airplaneController.Edit(testObject);

        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }

        var repository = new AirplaneRepository(context);
        var airplane = await repository.GetById(1);
        Assert.Equal(6666, airplane!.PassengerQuantity);
        Assert.Equal(changeCode, airplane.Code);
        Assert.Equal(changeModel, airplane.Model);
    }

    [Fact]
    public async Task AirplaneController_Edit_Error()
    {
        var sp = _fixture.InitiateConxtext("test_database_AirplaneController_Edit_Error");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var changeCode = "Code testObject edit";
        var changeModel = "Model testObject edit";

        var testObject = new AirplaneEditDto
        {
            Id = 1,
            Code = changeCode,
            PassengerQuantity = 6666
        };

        var airplaneController =
            AirplaneInjectionController.GetAirplaneController(context, mediator);
        var result = await airplaneController.Edit(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }

        var repository = new AirplaneRepository(context);
        var airplane = await repository.GetById(1);
        Assert.NotEqual(6666, airplane!.PassengerQuantity);
        Assert.NotEqual(changeCode, airplane.Code);
        Assert.NotEqual(changeModel, airplane.Model);
    }
}