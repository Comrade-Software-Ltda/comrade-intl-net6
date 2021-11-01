using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public sealed class SystemUserControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerCreateTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public async Task SystemUserController_Create()
    {
        var testObject = new SystemUserCreateDto
        {
            Name = "111",
            Email = "777@testObject",
            Password = "123456",
            Registration = "123"
        };

        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_Create");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);

        var result = await systemUserController.Create(testObject);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }


    [Fact]
    public async Task SystemUserController_Create_Error()
    {
        var testObject = new SystemUserCreateDto
        {
            Email = "777@testObject",
            Password = "123456",
            Registration = "123"
        };

        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_Create_Error");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);

        var result = await systemUserController.Create(testObject);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(400, actualResultValue?.Code);
        }
    }
}