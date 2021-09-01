#region

using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.EntityFrameworkCore;
using Xunit;

#endregion

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneControllerDeleteTests
{

    [Fact]
    public async Task AirplaneController_Delete()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_AirplaneController_Delete")
            .EnableSensitiveDataLogging().Options;


        var idAirplane = 1;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var airplaneController = AirplaneInjectionController.GetAirplaneController(context);
        _ = await airplaneController.Delete(idAirplane);

        var repository = new AirplaneRepository(context);
        var airplane = await repository.GetById(1);
        Assert.Null(airplane);
    }
}
