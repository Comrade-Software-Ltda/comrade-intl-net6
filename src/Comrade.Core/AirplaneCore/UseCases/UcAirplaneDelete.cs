using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneDelete(IMediator mediator) : UseCase, IUcAirplaneDelete
{
    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new AirplaneDeleteCommand {Id = id};
        return await mediator.Send(entity);
    }
}
