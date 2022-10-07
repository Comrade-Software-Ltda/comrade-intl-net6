using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Core.SystemUserSystemRoleCore.UseCases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Handlers;

public class SystemUserManageRolesHandler : IRequestHandler<SystemUserManageRolesDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserSystemRoleManage _manageSystemUserSystemRole;
    private readonly IMapper _mapper;

    public SystemUserManageRolesHandler(IMapper mapper, IUcSystemUserSystemRoleManage manageSystemUserSystemRole)
    {
        _mapper = mapper;
        _manageSystemUserSystemRole = manageSystemUserSystemRole;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserManageRolesDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserSystemRoleManageCommand>(request);
        var result = await _manageSystemUserSystemRole.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}