using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserSystemRoleIntegrationTests;

public sealed class SystemUserSystemRoleControllerCreateTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserSystemRoleControllerCreateTests()
    {
        _fixture = new ServiceProviderFixture();
    }

    [Fact]
    public async Task SystemUserSystemRoleController_Create()
    {
        var systemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");
        var systemRoleId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var testObject = new SystemUserSystemRoleCreateDto
        {
            SystemUserId = systemUserId,
            SystemRoleId = systemRoleId
        };
        var controller = SystemUserSystemRoleInjectionController.GetSystemUserSystemRoleController(_fixture.SqlContextFixture, _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Create(testObject);
        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(201, actualResultValue?.Code);
        }
    }
}