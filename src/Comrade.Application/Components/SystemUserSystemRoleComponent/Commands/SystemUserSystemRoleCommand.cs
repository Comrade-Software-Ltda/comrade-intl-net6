using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Core.SystemUserSystemRoleCore;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Commands;

public class SystemUserSystemRoleCommand : ISystemUserSystemRoleCommand
{
    private readonly IUcSystemUserSystemRoleDelete _deleteUc;
    private readonly IMediator _mediator;

    public SystemUserSystemRoleCommand(IUcSystemUserSystemRoleDelete deleteUc, IMediator mediator)
    {
        _deleteUc = deleteUc;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(SystemUserSystemRoleCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemUserSystemRoleEditDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await _deleteUc.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}