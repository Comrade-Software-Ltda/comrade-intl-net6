using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserDelete : UseCase, IUcSystemUserDelete
{
    private readonly IMediator _mediator;

    public UcSystemUserDelete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemUserDeleteCommand {Id = id};
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}