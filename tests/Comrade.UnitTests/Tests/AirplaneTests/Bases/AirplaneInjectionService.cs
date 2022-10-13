using AutoMapper;
using Comrade.Application.Components.AirplaneComponent.Commands;
using Comrade.Application.Components.AirplaneComponent.Queries;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class AirplaneInjectionService
{
    public static AirplaneCommand GetAirplaneCommand(ComradeContext context,
        IMediator mediator)
    {
        var ucAirplaneDelete =
            new UcAirplaneDelete(mediator);

        return new AirplaneCommand(ucAirplaneDelete, mediator);
    }

    public static AirplaneQuery GetAirplaneQuery(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMapper mapper)
    {
        var airplaneRepository = new AirplaneRepository(context);

        return new AirplaneQuery(airplaneRepository, mongoDbContextFixture, mapper);
    }
}
