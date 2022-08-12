using Comrade.Api.Controllers.V1.AirplaneApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class AirplaneInjectionController
{
    protected AirplaneInjectionController(){}
    public static AirplaneController GetAirplaneController(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMediator mediator)
    {
        var mapper = MapperHelper.ConfigMapper();
        var logger = Mock.Of<ILogger<AirplaneController>>();

        var airplaneCommand =
            AirplaneInjectionService.GetAirplaneCommand(context, mediator);
        var airplaneQuery =
            AirplaneInjectionService.GetAirplaneQuery(context!, mongoDbContextFixture, mapper);
        return new AirplaneController(airplaneCommand, airplaneQuery, logger);
    }
}