using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Components.SystemUserComponent.Contracts;
using Comrade.Core.SystemUserCore;
using Comrade.Core.SystemUserCore.Commands;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Handlers;

public class SystemUserManageRolesHandler : IRequestHandler<SystemUserManageRolesDto, SingleResultDto<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IUcSystemUserManageRoles _ucSystemUserManageRoles;

    public SystemUserManageRolesHandler(IMapper mapper, IUcSystemUserManageRoles ucSystemUserManageRoles)
    {
        _mapper = mapper;
        _ucSystemUserManageRoles = ucSystemUserManageRoles;
    }

    public async Task<SingleResultDto<EntityDto>> Handle(SystemUserManageRolesDto request,
        CancellationToken cancellationToken)
    {
        var mappedObject = _mapper.Map<SystemUserManageRolesCommand>(request);
        var result = await _ucSystemUserManageRoles.Execute(mappedObject).ConfigureAwait(false);
        return new SingleResultDto<EntityDto>(result);
    }
}
