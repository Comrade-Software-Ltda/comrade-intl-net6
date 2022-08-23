using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Handlers;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.TestDatas;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Comrade.UnitTests.Tests.SystemRoleTests;

public sealed class UcSystemRoleCreateTests
{
    [Theory]
    [ClassData(typeof(SystemRoleCreateTestData))]
    public async Task UcSystemRoleCreate_Test(int expected, SystemRoleCreateCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcSystemRoleCreate_Test" + testObjectInput.Id)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new SystemRoleRepository(context);
        var nameUniqueValidation = new SystemRoleNameUniqueValidation(repository);
        var createValidation = new SystemRoleCreateValidation(nameUniqueValidation);
        var mongo = new Mock<IMongoDbCommandContext>();

        var handler = new SystemRoleCreateCoreHandler(createValidation, repository, mongo.Object);
        var result = await handler.Handle(testObjectInput, CancellationToken.None);

        Assert.Equal(expected, result.Code);
    }
}
