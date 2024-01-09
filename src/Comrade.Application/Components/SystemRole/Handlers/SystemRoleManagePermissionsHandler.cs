using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRole.Contracts;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemRole.Handlers;

public class SystemRoleManagePermissionsHandler(
    IMapper mapper,
    IUcSystemRoleManagePermissions ucSystemRoleManagePermissions)
    : IRequestHandler<SystemRoleManagePermissionsDto,
        SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemRoleManagePermissionsDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemRoleManagePermissionsCommand>(request);
        var result = await ucSystemRoleManagePermissions.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
