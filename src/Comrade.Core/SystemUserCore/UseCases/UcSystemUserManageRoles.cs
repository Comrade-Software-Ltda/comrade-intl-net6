using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserManageRoles : UseCase, IUcSystemUserManageRoles
{
    private readonly IMediator _mediator;

    public UcSystemUserManageRoles(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUserManageRolesCommand entity)
    {
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
