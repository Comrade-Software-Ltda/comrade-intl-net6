using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.Commands;

public class SystemUserManageRolesCommand : Entity, IRequest<ISingleResult<Entity>>
{
    public ICollection<Guid> SystemRoleIds { get; set; }
}
