using System;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserSystemRoleIntegrationTests;

public class SystemUserSystemRoleControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserSystemRoleControllerDeleteTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUserSystemRoleController_Delete()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4");
        var controller = SystemUserSystemRoleInjectionController.GetSystemUserSystemRoleController(_fixture.SqlContextFixture, _fixture.MongoDbContextFixture, _fixture.Mediator);
        _ = await controller.Delete(id);
        var repository = new SystemUserSystemRoleRepository(_fixture.SqlContextFixture);
        var obj = await repository.GetById(id);
        Assert.Null(obj);
    }
}