using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerGetTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerGetTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemUserController_Get()
    {
        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_Get");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var systemUserId = 1;
        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);
        var result = await systemUserController.GetById(systemUserId);

        if (result is OkObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<SystemUserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
        }
    }
}