using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.Commands;

public class SystemUserSystemRoleManageCommand : Entity, IRequest<ISingleResult<Entity>>
{
    public SystemUserSystemRoleManageCommand(ICollection<Guid> roles)
    {
        Roles = roles;
    }

    public ICollection<Guid> Roles { get; set; }

}