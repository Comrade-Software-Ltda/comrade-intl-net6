using Comrade.Core.AirplaneCore.UseCases;
using Comrade.Persistence.DataAccess;
using MediatR;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class UcAirplaneInjection
{
    public static UcAirplaneCreate GetUcAirplaneCreate(ComradeContext context, IMediator mediator)
    {
        return new UcAirplaneCreate(mediator);
    }

    public static UcAirplaneEdit GetUcAirplaneEdit(ComradeContext context, IMediator mediator)
    {
        return new UcAirplaneEdit(mediator);
    }
}