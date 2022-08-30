using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.Commands;

public class SystemUserSystemRoleEditCommand : SystemUserSystemRole, IRequest<ISingleResult<Entity>>
{
}
