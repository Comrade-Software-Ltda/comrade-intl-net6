using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemUser_Context()
    {
        var sp = _fixture.InitiateConxtext("test_database_SystemUser_Context");
        var context = sp.GetService<ComradeContext>()!;
        InjectDataOnContextBase.InitializeDbForTests(context);
        var repository = new SystemUserRepository(context);
        var systemUser = await repository.GetById(1);
        Assert.NotNull(systemUser);
    }
}