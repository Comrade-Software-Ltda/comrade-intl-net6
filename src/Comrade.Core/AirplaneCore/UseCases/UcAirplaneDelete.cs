using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.AirplaneCore.UseCases;

public class UcAirplaneDelete : UseCase, IUcAirplaneDelete
{
    private readonly IMediator _mediator;

    public UcAirplaneDelete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new AirplaneDeleteCommand {Id = id};
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}