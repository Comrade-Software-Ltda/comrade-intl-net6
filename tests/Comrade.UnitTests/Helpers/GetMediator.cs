using MediatR;

namespace Comrade.UnitTests.Helpers;

public static class GetMediator
{
    public static IMediator Execute(ServiceProvider sp)
    {
        var mediator = sp.GetRequiredService<IMediator>();
        return mediator;
    }
}