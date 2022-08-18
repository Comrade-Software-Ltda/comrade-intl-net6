using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Core.SystemRoleCore.Handlers;
using Comrade.Core.SystemRoleCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemRoleTests.TestDatas;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Comrade.UnitTests.Tests.SystemRoleTests;

public sealed class UcSystemRoleEditTests
{
    [Theory]
    [ClassData(typeof(SystemRoleEditTestData))]
    public async Task UcSystemRoleEdit_Test(int expected, SystemRoleEditCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcSystemRoleEdit_Test" + testObjectInput.Id)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new SystemRoleRepository(context);
        var validation = new Mock<ISystemRoleEditValidation>();
        var mongo = new Mock<IMongoDbCommandContext>();

        validation.Setup(s =>
                s.Execute(It.IsAny<SystemRole>(), It.IsAny<SystemRole>()))
            .Returns(new SingleResult<Entity>(testObjectInput));

        var handler = new SystemRoleEditCoreHandler(validation.Object, repository, mongo.Object);
        var result = await handler.Handle(testObjectInput, CancellationToken.None);

        Assert.Equal(result.Code, expected);
    }
}