using Comrade.Core.Bases;
using Comrade.Core.Bases.Interfaces;
using Comrade.Core.SystemUserSystemPermissionCore.Commands;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemPermissionCore.UseCases;

public class UcSystemUserSystemPermissionManage : UseCase, IUcSystemUserSystemPermissionManage
{
    private readonly IMediator _mediator;

    public UcSystemUserSystemPermissionManage(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemUserSystemPermissionManageCommand entity)
    {
        return await _mediator.Send(entity).ConfigureAwait(false);
    }
}