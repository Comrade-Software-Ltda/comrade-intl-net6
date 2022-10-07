using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserSystemPermissionCore.Commands;
using Comrade.Core.SystemUserSystemPermissionCore.UseCases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Handlers;

public class SystemUserManagePermissionsHandler : IRequestHandler<SystemUserManagePermissionsDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserSystemPermissionManage _manageSystemUserSystemPermission;
    private readonly IMapper _mapper;

    public SystemUserManagePermissionsHandler(IMapper mapper, IUcSystemUserSystemPermissionManage manageSystemUserSystemPermission)
    {
        _mapper = mapper;
        _manageSystemUserSystemPermission = manageSystemUserSystemPermission;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserManagePermissionsDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserSystemPermissionManageCommand>(request);
        var result = await _manageSystemUserSystemPermission.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}