using System;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemPermissionIntegrationTests;

public class SystemPermissionContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemPermissionContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemPermission_Context()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1");
        var repository = new SystemPermissionRepository(_fixture.SqlContextFixture);
        var obj = await repository.GetById(id);
        Assert.NotNull(obj);
    }
}
