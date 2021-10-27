using Comrade.Api.UseCases.V1.AirplaneApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class AirplaneInjectionController
{
    public static AirplaneController GetAirplaneController(ComradeContext ctx)
    {
        throw new NotImplementedException();
    }

    public static AirplaneController GetAirplaneController()
    {
        var mapper = MapperHelper.ConfigMapper();

        var sp = GetServiceProvider.Execute();
        var context = GetContext.Execute(sp);
        var mediator = GetMediator.Execute(sp);

        var logger = Mock.Of<ILogger<AirplaneController>>();


        var airplaneCommand =
            AirplaneInjectionService.GetAirplaneCommand(context!, mapper, mediator);
        var airplaneQuery = AirplaneInjectionService.GetAirplaneQuery(context!, mapper);
        return new AirplaneController(airplaneCommand, airplaneQuery, logger);
    }
}