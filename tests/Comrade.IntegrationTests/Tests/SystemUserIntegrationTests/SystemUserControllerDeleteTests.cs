using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Xunit;

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerDeleteTests
{
    [Fact]
    public async Task SystemUserController_Delete()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_SystemUserController_Delete")
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var systemUserController =
            SystemUserInjectionController.GetSystemUserController(context);
        _ = await systemUserController.Delete(1);

        var repository = new SystemUserRepository(context);
        var user = await repository.GetById(1);
        Assert.Null(user);
    }
}