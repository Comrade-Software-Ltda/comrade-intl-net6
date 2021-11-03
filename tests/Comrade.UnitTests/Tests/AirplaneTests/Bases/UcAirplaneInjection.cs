using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Persistence.DataAccess;
using MediatR;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class UcAirplaneInjection
{
    public static UcAirplaneCreate GetUcAirplaneCreate(ComradeContext context, IMediator mediator)
    {
        var uow = new UnitOfWork(context);

        return new UcAirplaneCreate(mediator, uow);
    }

    public static UcAirplaneEdit GetUcAirplaneEdit(ComradeContext context, IMediator mediator)
    {
        var uow = new UnitOfWork(context);

        return new UcAirplaneEdit(mediator, uow);
    }
}