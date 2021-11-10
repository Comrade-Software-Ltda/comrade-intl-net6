using Comrade.Application.Bases;
using Comrade.Application.Services.SystemUserServices.Dtos;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerGetTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerGetTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserController_Get()
    {
        var systemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");
        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture,
                _fixture.Mediator);
        var result = await systemUserController.GetById(systemUserId);

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<SystemUserDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
        }
    }
}