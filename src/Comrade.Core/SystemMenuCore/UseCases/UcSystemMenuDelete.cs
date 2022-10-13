using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemMenuCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemMenuCore.UseCases;

public class UcSystemMenuDelete : UseCase, IUcSystemMenuDelete
{
    private readonly IMediator _mediator;

    public UcSystemMenuDelete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemMenuDeleteCommand {Id = id};
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
