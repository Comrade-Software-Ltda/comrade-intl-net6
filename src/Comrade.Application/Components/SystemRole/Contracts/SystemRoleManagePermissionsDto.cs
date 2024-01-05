using Comrade.Application.Bases;
using MediatR;

namespace Comrade.Application.Components.SystemRole.Contracts;

public class SystemRoleManagePermissionsDto : EntityDto, IRequest<SingleResultDto<EntityDto>>
{
    public ICollection<Guid> SystemPermissionIds { get; set; }
}
