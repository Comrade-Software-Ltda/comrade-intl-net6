using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Core.AirplaneCore.Validations;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class UcAirplaneInjection
{
    public static UcAirplaneCreate GetUcAirplaneCreate(ComradeContext context)
    {
        var uow = new UnitOfWork(context);
        var airplaneRepository = new AirplaneRepository(context);

        var airplaneValidateSameCode = new AirplaneValidateSameCode(airplaneRepository);

        var airplaneCreateValidation =
            new AirplaneCreateValidation(airplaneRepository, airplaneValidateSameCode);

        return new UcAirplaneCreate(airplaneRepository, airplaneCreateValidation, uow);
    }

    public static UcAirplaneEdit GetUcAirplaneEdit(ComradeContext context)
    {
        var uow = new UnitOfWork(context);
        var airplaneRepository = new AirplaneRepository(context);

        var airplaneValidateSameCode = new AirplaneValidateSameCode(airplaneRepository);

        var airplaneEditValidation =
            new AirplaneEditValidation(airplaneRepository, airplaneValidateSameCode);

        return new UcAirplaneEdit(airplaneRepository, airplaneEditValidation, uow);
    }
}