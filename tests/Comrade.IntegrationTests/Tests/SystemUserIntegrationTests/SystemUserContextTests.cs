#region

using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.SystemUserIntegrationTests;

public class SystemUserContextTests
{
    private readonly SystemUserInjectionController _systemUserInjectionController = new();

    [Fact]
    public async Task SystemUser_Context()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_SystemUser_Context")
            .EnableSensitiveDataLogging().Options;


        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);
        var repository = new SystemUserRepository(context);
        var systemUser = await repository.GetById(1);
        Assert.NotNull(systemUser);
    }
}
