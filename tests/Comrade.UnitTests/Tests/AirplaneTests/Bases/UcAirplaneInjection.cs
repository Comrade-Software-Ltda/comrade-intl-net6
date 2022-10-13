using Comrade.Core.AirplaneCore.UseCases;
using MediatR;

namespace Comrade.UnitTests.Tests.AirplaneTests.Bases;

public sealed class UcAirplaneInjection
{
    public static UcAirplaneCreate GetUcAirplaneCreate(IMediator mediator)
    {
        return new UcAirplaneCreate(mediator);
    }

    public static UcAirplaneEdit GetUcAirplaneEdit(IMediator mediator)
    {
        return new UcAirplaneEdit(mediator);
    }
}
