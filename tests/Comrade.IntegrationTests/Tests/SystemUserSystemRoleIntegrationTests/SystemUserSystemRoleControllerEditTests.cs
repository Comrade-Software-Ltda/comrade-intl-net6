using System;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserSystemRoleIntegrationTests;

public class SystemUserSystemRoleControllerEditTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserSystemRoleControllerEditTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserSystemRoleController_Edit()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4");
        var systemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");
        var systemRoleId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var testObject = new SystemUserSystemRoleEditDto
        {
            Id = id,
            SystemUserId = systemUserId,
            SystemRoleId = systemRoleId
        };
        var controller = SystemUserSystemRoleInjectionController.GetSystemUserSystemRoleController(_fixture.SqlContextFixture, _fixture.MongoDbContextFixture, _fixture.Mediator);
        var result = await controller.Edit(testObject);
        if (result is ObjectResult objectResult)
        {
            var actualResultValue = objectResult.Value as SingleResultDto<EntityDto>;
            Assert.NotNull(actualResultValue);
            Assert.Equal(204, actualResultValue?.Code);
        }
        var repository = new SystemUserSystemRoleRepository(_fixture.SqlContextFixture);
        var obj = await repository.GetById(id);
        Assert.Equal(systemUserId, obj!.SystemUserId);
        Assert.Equal(systemRoleId, obj!.SystemRoleId);
    }
}