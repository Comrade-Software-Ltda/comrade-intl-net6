using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerDeleteTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.PostgresContextFixture);
    }

    [Fact]
    public async Task SystemUserController_Delete()
    {
        var idSystemUser = 1;

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(_fixture.PostgresContextFixture, _fixture.Mediator);
        _ = await systemUserController.Delete(idSystemUser);

        var repository = new SystemUserRepository(_fixture.PostgresContextFixture);
        var systemUser = await repository.GetById(1);
        Assert.Null(systemUser);
    }
}