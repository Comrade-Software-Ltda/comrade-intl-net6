using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;
using Comrade.Core.SystemPermissionCore;
using MediatR;

namespace Comrade.Application.Components.SystemPermissionComponent.Commands;

public class SystemPermissionCommand : ISystemPermissionCommand
{
    private readonly IUcSystemPermissionDelete _deleteUc;
    private readonly IMediator _mediator;

    public SystemPermissionCommand(IUcSystemPermissionDelete deleteUc, IMediator mediator)
    {
        _deleteUc = deleteUc;
        _mediator = mediator;
    }

    public async Task<ISingleResultDto<EntityDto>> Create(SystemPermissionCreateDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemPermissionEditDto dto)
    {
        return await _mediator.Send(dto).ConfigureAwait(false);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await _deleteUc.Execute(id).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
