#region

using AutoMapper;
using Comrade.Application.Services.AirplaneServices.Commands;
using Comrade.Application.Services.AirplaneServices.Queries;
using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;

#endregion

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class AirplaneInjectionService
{
    public AirplaneCommand GetAirplaneCommand(ComradeContext context, IMapper mapper)
    {
        var uow = new UnitOfWork(context);
        var airplaneRepository = new AirplaneRepository(context);

        var airplaneValidateSameCode = new AirplaneValidateSameCode(airplaneRepository);

        var airplaneEditValidation =
            new AirplaneEditValidation(airplaneRepository, airplaneValidateSameCode);
        var airplaneDeleteValidation = new AirplaneDeleteValidation(airplaneRepository);
        var airplaneCreateValidation =
            new AirplaneCreateValidation(airplaneRepository, airplaneValidateSameCode);
        var ucAirplaneCreate =
            new UcAirplaneCreate(airplaneRepository, airplaneCreateValidation, uow);
        var ucAirplaneDelete =
            new UcAirplaneDelete(airplaneRepository, airplaneDeleteValidation, uow);
        var ucAirplaneEdit =
            new UcAirplaneEdit(airplaneRepository, airplaneEditValidation, uow);

        return new AirplaneCommand(ucAirplaneEdit, ucAirplaneCreate,
            ucAirplaneDelete, mapper);
    }

    public AirplaneQuery GetAirplaneQuery(ComradeContext context, IMapper mapper)
    {
        var airplaneRepository = new AirplaneRepository(context);

        return new AirplaneQuery(airplaneRepository, mapper);
    }
}
