using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Core.SystemPermissionCore.Handlers;
using Comrade.Core.SystemPermissionCore.Validations;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemPermissionTests.TestDatas;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Comrade.UnitTests.Tests.SystemPermissionTests;

public sealed class UcSystemPermissionCreateTests
{
    [Theory]
    [ClassData(typeof(SystemPermissionCreateTestData))]
    public async Task UcSystemPermissionCreate_Test(int expected, SystemPermissionCreateCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcSystemPermissionCreate_Test" + testObjectInput.Id)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new SystemPermissionRepository(context);
        var tagUniqueValidation = new SystemPermissionTagUniqueValidation(repository);
        var createValidation = new SystemPermissionCreateValidation(tagUniqueValidation);
        var mongo = new Mock<IMongoDbCommandContext>();

        var handler = new SystemPermissionCreateCoreHandler(createValidation, repository, mongo.Object);
        var result = await handler.Handle(testObjectInput, CancellationToken.None);

        Assert.Equal(expected, result.Code);
    }
}
