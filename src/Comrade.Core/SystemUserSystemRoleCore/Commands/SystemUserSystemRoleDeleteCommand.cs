using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserSystemRoleCore.Commands;

public class SystemUserSystemRoleDeleteCommand : SystemUserSystemRole, IRequest<ISingleResult<Entity>>
{
}
