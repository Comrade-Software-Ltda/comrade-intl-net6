using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserCore.Commands;

public class SystemUserManageRolesCommand : Entity, IRequest<ISingleResult<Entity>>
{
    public SystemUserManageRolesCommand(ICollection<Guid> roles)
    {
        Roles = roles;
    }

    public ICollection<Guid> Roles { get; set; }
}
