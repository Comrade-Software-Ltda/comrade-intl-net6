using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemRoleComponent.Contracts;
using Comrade.Core.SystemRoleCore;
using Comrade.Core.SystemRoleCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Handlers;

public class SystemRoleManagePermissionsHandler : IRequestHandler<SystemRoleManagePermissionsDto,
    SingleResultDto<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IUcSystemRoleManagePermissions _ucSystemRoleManagePermissions;

    public SystemRoleManagePermissionsHandler(IMapper mapper,
        IUcSystemRoleManagePermissions ucSystemRoleManagePermissions)
    {
        _mapper = mapper;
        _ucSystemRoleManagePermissions = ucSystemRoleManagePermissions;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemRoleManagePermissionsDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemRoleManagePermissionsCommand>(request);
        var result = await _ucSystemRoleManagePermissions.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
