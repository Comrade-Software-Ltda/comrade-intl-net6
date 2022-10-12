using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.SystemRoleCore;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Commands;

public class SystemRoleCommand : ISystemRoleCommand
{
    private readonly IUcSystemRoleDelete _deleteUc;
    private readonly IMediator _mediator;

    public SystemRoleCommand(IUcSystemRoleDelete deleteUc, IMediator mediator)
    {
        _deleteUc = deleteUc;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(SystemRoleCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemRoleEditDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await _deleteUc.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
