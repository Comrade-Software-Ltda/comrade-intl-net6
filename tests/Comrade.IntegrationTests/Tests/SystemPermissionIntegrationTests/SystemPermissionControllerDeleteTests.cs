using System;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemPermissionTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemPermissionIntegrationTests;

public class SystemPermissionControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemPermissionControllerDeleteTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemPermissionController_Delete()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        var controller = SystemPermissionInjectionController.GetSystemPermissionController(_fixture.SqlContextFixture,
            _fixture.MongoDbContextFixture, _fixture.Mediator);
        _ = await controller.Delete(id);
        var repository = new SystemPermissionRepository(_fixture.SqlContextFixture);
        var obj = await repository.GetById(id);
        Assert.Null(obj);
    }
}
