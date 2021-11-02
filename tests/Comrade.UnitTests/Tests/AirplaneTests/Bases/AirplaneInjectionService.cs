using AutoMapper;
using Comrade.Application.Services.AirplaneServices.Commands;
using Comrade.Application.Services.AirplaneServices.Queries;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class AirplaneInjectionService
{
    public static AirplaneCommand GetAirplaneCommand(ComradeContext context, IMapper mapper,
        IMediator mediator)
    {
        var uow = new UnitOfWork(context);
        var airplaneRepository = new AirplaneRepository(context);
        var airplaneDeleteValidation = new AirplaneDeleteValidation();
        var ucAirplaneDelete =
            new UcAirplaneDelete(airplaneRepository, airplaneDeleteValidation, uow);

        return new AirplaneCommand(ucAirplaneDelete, mapper, mediator);
    }

    public static AirplaneQuery GetAirplaneQuery(ComradeContext context, IMapper mapper)
    {
        var airplaneRepository = new AirplaneRepository(context);

        return new AirplaneQuery(airplaneRepository, mapper);
    }
}