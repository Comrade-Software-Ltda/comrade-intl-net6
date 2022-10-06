using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;
using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Core.SystemUserSystemRoleCore.UseCases;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Handlers;

public class SystemUserSystemRoleManageHandler : IRequestHandler<SystemUserSystemRoleManageDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserSystemRoleManage _manageSystemUserSystemRole;
    private readonly IMapper _mapper;

    public SystemUserSystemRoleManageHandler(IMapper mapper, IUcSystemUserSystemRoleManage manageSystemUserSystemRole)
    {
        _mapper = mapper;
        _manageSystemUserSystemRole = manageSystemUserSystemRole;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserSystemRoleManageDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserSystemRoleManageCommand>(request);
        var result = await _manageSystemUserSystemRole.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}