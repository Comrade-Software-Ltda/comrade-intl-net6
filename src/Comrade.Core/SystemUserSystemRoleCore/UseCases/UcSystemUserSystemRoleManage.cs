using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.UseCases;

public class UcSystemUserSystemRoleManage : UseCase, IUcSystemUserSystemRoleManage
{
    private readonly IMediator _mediator;

    public UcSystemUserSystemRoleManage(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUserSystemRoleManageCommand entity)
    {
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}