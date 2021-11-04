using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using System;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemUser_Context()
    {
        var id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5");
        var repository = new SystemUserRepository(_fixture.SqlContextFixture);
        var systemUser = await repository.GetById(id);
        Assert.NotNull(systemUser);
    }
}