using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerDeleteTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public SystemUserControllerDeleteTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task SystemUserController_Delete()
    {
        var sp = _fixture.InitiateConxtext("test_database_SystemUserController_Delete");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;

        InjectDataOnContextBase.InitializeDbForTests(context);

        var idSystemUser = 1;

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context, mediator);
        _ = await systemUserController.Delete(idSystemUser);

        var repository = new SystemUserRepository(context);
        var systemUser = await repository.GetById(1);
        Assert.Null(systemUser);
    }
}