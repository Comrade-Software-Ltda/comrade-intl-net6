using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Commands;

public class SystemRoleManagePermissionsCommand : Entity, IRequest<ISingleResult<Entity>>
{
    public ICollection<Guid> SystemPermissionIds { get; set; }
}
