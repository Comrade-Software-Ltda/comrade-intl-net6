using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Handlers;

public class
    SystemUserManagePermissionsHandler : IRequestHandler<SystemUserManagePermissionsDto, SingleResultDto<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IUcSystemUserManagePermissions _ucSystemUserManagePermissions;

    public SystemUserManagePermissionsHandler(IMapper mapper,
        IUcSystemUserManagePermissions ucSystemUserManagePermissions)
    {
        _mapper = mapper;
        _ucSystemUserManagePermissions = ucSystemUserManagePermissions;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserManagePermissionsDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserManagePermissionsCommand>(request);
        var result = await _ucSystemUserManagePermissions.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
