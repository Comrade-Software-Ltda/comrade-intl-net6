using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.SystemUserSystemPermissionCore.Commands;

public class SystemUserSystemPermissionManageCommand : Entity, IRequest<ISingleResult<Entity>>
{
    public SystemUserSystemPermissionManageCommand(ICollection<Guid> permissions)
    {
        Permissions = permissions;
    }
    public ICollection<Guid> Permissions { get; set; }

}