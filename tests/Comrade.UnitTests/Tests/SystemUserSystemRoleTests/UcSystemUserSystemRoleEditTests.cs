using System.Threading;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Core.SystemUserSystemRoleCore.Handlers;
using Comrade.Core.SystemUserSystemRoleCore.Validations;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.SystemUserSystemRoleTests.TestDatas;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Comrade.UnitTests.Tests.SystemUserSystemRoleTests;

public sealed class UcSystemUserSystemRoleEditTests
{
    [Theory]
    [ClassData(typeof(SystemUserSystemRoleEditTestData))]
    public async Task UcSystemUserSystemRoleEdit_Test(int expected, SystemUserSystemRoleEditCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcSystemUserSystemRoleEdit_Test" + testObjectInput.Id)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new SystemUserSystemRoleRepository(context);
        var validation = new Mock<ISystemUserSystemRoleEditValidation>();
        validation.Setup(s =>
                s.Execute(It.IsAny<SystemUserSystemRole>(), It.IsAny<SystemUserSystemRole>()))
            .ReturnsAsync(new SingleResult<Entity>(testObjectInput));
        var mongo = new Mock<IMongoDbCommandContext>();

        var handler = new SystemUserSystemRoleEditCoreHandler(validation.Object, repository, mongo.Object);
        var result = await handler.Handle(testObjectInput, CancellationToken.None);

        Assert.Equal(expected, result.Code);
    }
}