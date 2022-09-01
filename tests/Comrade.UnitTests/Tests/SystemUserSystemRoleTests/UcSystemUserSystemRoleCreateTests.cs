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

public sealed class UcSystemUserSystemRoleCreateTests
{
    [Theory]
    [ClassData(typeof(SystemUserSystemRoleCreateTestData))]
    public async Task UcSystemUserSystemRoleCreate_Test(int expected, SystemUserSystemRoleCreateCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcSystemUserSystemRoleCreate_Test" + testObjectInput.Id)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new SystemUserSystemRoleRepository(context);
        var validation = new Mock<ISystemUserSystemRoleCreateValidation>();
        validation.Setup(s =>
                s.Execute(It.IsAny<SystemUserSystemRole>()))
            .ReturnsAsync(new SingleResult<Entity>(testObjectInput));
        var mongo = new Mock<IMongoDbCommandContext>();

        var handler = new SystemUserSystemRoleCreateCoreHandler(validation.Object, repository, mongo.Object);
        var result = await handler.Handle(testObjectInput, CancellationToken.None);

        Assert.Equal(expected, result.Code);
    }
}