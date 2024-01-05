using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.SystemRoleCore;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Commands;

public class SystemRoleCommand(IUcSystemRoleDelete deleteUc, IMediator mediator) : ISystemRoleCommand
{
    public async Task<ISingleResultDto<EntityDto>> Create(SystemRoleCreateDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemRoleEditDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await deleteUc.Execute(id);
        return new SingleResultDto<EntityDto>(result);
    }

    public async Task<ISingleResultDto<EntityDto>> ManagePermissions(SystemRoleManagePermissionsDto dto)
    {
        return await mediator.Send(dto);
    }
}
