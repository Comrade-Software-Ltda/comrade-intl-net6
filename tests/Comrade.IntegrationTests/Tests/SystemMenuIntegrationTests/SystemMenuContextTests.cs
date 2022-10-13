using System;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemMenuIntegrationTests;

public class SystemMenuContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemMenuContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemMenu_Context()
    {
        var systemMenuId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a6");
        var repository = new SystemMenuRepository(_fixture.SqlContextFixture);
        var systemMenu = await repository.GetById(systemMenuId);
        Assert.NotNull(systemMenu);
    }
}
