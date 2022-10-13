using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemPermissionTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemPermissionIntegrationTests;

public class SystemPermissionControllerGetTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemPermissionControllerGetTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemPermissionController_Get()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.GetById(id);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<SystemPermissionDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(200, actualResultValue?.Code);
            Assert.NotNull(actualResultValue?.Data);
        }
    }
}
