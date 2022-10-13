using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.UseCases;

public class UcSystemUserManagePermissions : UseCase, IUcSystemUserManagePermissions
{
    private readonly IMediator _mediator;

    public UcSystemUserManagePermissions(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUserManagePermissionsCommand entity)
    {
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}
