using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserManageRolesDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
{
    public SystemUserManageRolesDto(ICollection<Guid> roles)
    {
        Roles = roles;
    }

    public ICollection<Guid> Roles { get; set; }
}
