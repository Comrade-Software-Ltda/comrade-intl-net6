using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserCore;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Commands;

public class SystemUserCommand(IUcSystemUserDelete deleteSystemUser, IMediator mediator) : ISystemUserCommand
{
    public async Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await deleteSystemUser.Execute(id);
        return new SingleResultDto<EntityDto>(result);
    }

    public async Task<ISingleResultDto<EntityDto>> ManagePermissions(SystemUserManagePermissionsDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> ManageRoles(SystemUserManageRolesDto dto)
    {
        return await mediator.Send(dto);
    }
}
