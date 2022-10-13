using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.UseCases;

public class UcSystemRoleDelete : UseCase, IUcSystemRoleDelete
{
    private readonly IMediator _mediator;

    public UcSystemRoleDelete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemRoleDeleteCommand {Id = id};
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
