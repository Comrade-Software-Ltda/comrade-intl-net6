using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemRoleComponent.Contracts;

public class SystemRoleManagePermissionsDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
{
    public ICollection<Guid> SystemPermissionIds { get; set; }
}
