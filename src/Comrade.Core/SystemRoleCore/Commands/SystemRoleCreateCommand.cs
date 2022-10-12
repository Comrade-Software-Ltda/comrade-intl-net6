using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemRoleCore.Commands;

public class SystemRoleCreateCommand : SystemRole, IRequest<ISingleResult<Entity>>
{
}
