using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermission.Contracts;
using Comrade.Core.SystemPermissionCore;
using MediatR;

namespace Comrade.Application.Components.SystemPermission.Commands;

public class SystemPermissionCommand(IUcSystemPermissionDelete deleteUc, IMediator mediator) : ISystemPermissionCommand
{
    public async Task<ISingleResultDto<EntityDto>> Create(SystemPermissionCreateDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Edit(SystemPermissionEditDto dto)
    {
        return await mediator.Send(dto);
    }

    public async Task<ISingleResultDto<EntityDto>> Delete(Guid id)
    {
        var result = await deleteUc.Execute(id);
        return new SingleResultDto<EntityDto>(result);
    }
}
