#region

using Comrade.Api.UseCases.V1.AirplaneApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using Microsoft.Extensions.Logging;
using Moq;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public class AirplaneInjectionController
{

    public static AirplaneController GetAirplaneController(ComradeContext context)
    {
        var mapper = MapperHelper.ConfigMapper();

        var logger = Mock.Of<ILogger<AirplaneController>>();

        var airplaneCommand = AirplaneInjectionService.GetAirplaneCommand(context, mapper);
        var airplaneQuery = AirplaneInjectionService.GetAirplaneQuery(context, mapper);

        return new AirplaneController(airplaneCommand, airplaneQuery, mapper, logger);
    }
}
