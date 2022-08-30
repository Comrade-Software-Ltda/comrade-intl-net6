using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.UseCases;

public class UcSystemUserSystemRoleDelete : UseCase, IUcSystemUserSystemRoleDelete
{
    private readonly IMediator _mediator;

    public UcSystemUserSystemRoleDelete(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(Guid id)
    {
        var entity = new SystemUserSystemRoleDeleteCommand { Id = id };
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}