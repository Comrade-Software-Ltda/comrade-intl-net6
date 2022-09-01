using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserSystemRoleIntegrationTests;

public class SystemUserSystemRoleControllerGetTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserSystemRoleControllerGetTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserSystemRoleController_Get()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4");
        var controller = SystemUserSystemRoleInjectionController.GetSystemUserSystemRoleController(_fixture.SqlContextFixture, _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.GetById(id);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<SystemUserSystemRoleDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
        }
    }
}