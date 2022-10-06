using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts;
using Comrade.Core.SystemUserSystemPermissionCore.Commands;
using Comrade.Core.SystemUserSystemPermissionCore.UseCases;
using MediatR;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Handlers;

public class SystemUserSystemPermissionManageHandler : IRequestHandler<SystemUserSystemPermissionManageDto, SingleResultDto<EntityDto>>
{
    private readonly IUcSystemUserSystemPermissionManage _manageSystemUserSystemPermission;
    private readonly IMapper _mapper;

    public SystemUserSystemPermissionManageHandler(IMapper mapper, IUcSystemUserSystemPermissionManage manageSystemUserSystemPermission)
    {
        _mapper = mapper;
        _manageSystemUserSystemPermission = manageSystemUserSystemPermission;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserSystemPermissionManageDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserSystemPermissionManageCommand>(request);
        var result = await _manageSystemUserSystemPermission.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}