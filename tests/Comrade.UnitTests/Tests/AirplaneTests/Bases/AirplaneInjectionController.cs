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
    private readonly AirplaneInjectionService _airplaneInjectionService = new();

    public AirplaneController GetAirplaneController(ComradeContext context)
    {
        var mapper = MapperHelper.ConfigMapper();

        var logger = Mock.Of<ILogger<AirplaneController>>();

        var airplaneCommand = _airplaneInjectionService.GetAirplaneCommand(context, mapper);
        var airplaneQuery = _airplaneInjectionService.GetAirplaneQuery(context, mapper);

        return new AirplaneController(airplaneCommand, airplaneQuery, mapper, logger);
    }
}
