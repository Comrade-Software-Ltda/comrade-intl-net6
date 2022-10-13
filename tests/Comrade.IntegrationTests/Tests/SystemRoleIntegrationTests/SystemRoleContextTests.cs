using System;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemRoleIntegrationTests;

public class SystemRoleContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemRoleContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task SystemRole_Context()
    {
        var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
        var repository = new SystemRoleRepository(_fixture.SqlContextFixture);
        var systemRole = await repository.GetById(id);
        Assert.NotNull(systemRole);
    }
}
