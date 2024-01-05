using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUser.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUser.Handlers;

public class
    SystemUserManagePermissionsHandler(
        IMapper mapper,
        IUcSystemUserManagePermissions ucSystemUserManagePermissions)
    : IRequestHandler<SystemUserManagePermissionsDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserManagePermissionsDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemUserManagePermissionsCommand>(request);
        var result = await ucSystemUserManagePermissions.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
