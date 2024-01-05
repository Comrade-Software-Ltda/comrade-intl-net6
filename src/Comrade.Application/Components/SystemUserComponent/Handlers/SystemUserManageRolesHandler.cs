using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Handlers;

public class SystemUserManageRolesHandler(IMapper mapper, IUcSystemUserManageRoles ucSystemUserManageRoles)
    : IRequestHandler<SystemUserManageRolesDto, SingleResultDto<EntityDto>>
{
    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserManageRolesDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = mapper.Map<SystemUserManageRolesCommand>(request);
        var result = await ucSystemUserManageRoles.Execute(mappedObject);
        return new SingleResultDto<EntityDto>(result);
    }
}
