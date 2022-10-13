using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemUserComponent.Contracts;

public class SystemUserManagePermissionsDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
{
    public SystemUserManagePermissionsDto(ICollection<Guid> permissions)
    {
        Permissions = permissions;
    }

    public ICollection<Guid> Permissions { get; set; }
}
