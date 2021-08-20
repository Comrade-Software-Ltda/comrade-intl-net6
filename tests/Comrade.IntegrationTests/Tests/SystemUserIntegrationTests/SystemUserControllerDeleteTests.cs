#region

using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserControllerDeleteTests
{
    private readonly SystemUserInjectionController _systemUserInjectionController = new();

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
            _systemUserInjectionController.GetSystemUserController(context);
        _ = await systemUserController.Delete(1);

        var respository = new SystemUserRepository(context);
        var user = await respository.GetById(1);
        Assert.Null(user);
    }
}
