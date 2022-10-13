using System.Threading;
using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.AirplaneCore.Handlers;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.TestDatas;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Xunit;

namespace Comrade.UnitTests.Tests.AirplaneTests;

public sealed class UcAirplaneEditTests
{
    [Theory]
    [ClassData(typeof(AirplaneEditTestData))]
    public async Task UcAirplaneEdit_Test(int expected, AirplaneEditCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcAirplaneEdit_Test" + testObjectInput.Id)
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var repository = new AirplaneRepository(context);

        var validation = new Mock<IAirplaneEditValidation>();
        var mongo = new Mock<IMongoDbCommandContext>();

        validation.Setup(s =>
                s.Execute(It.IsAny<Airplane>(), It.IsAny<Airplane>()))
            .ReturnsAsync(new SingleResult<Entity>(testObjectInput));

        var handler =
            new AirplaneEditCoreHandler(validation.Object, repository, mongo.Object);

        var result = await handler.Handle(testObjectInput, CancellationToken.None);

        Assert.Equal(result.Code, expected);
    }
}
