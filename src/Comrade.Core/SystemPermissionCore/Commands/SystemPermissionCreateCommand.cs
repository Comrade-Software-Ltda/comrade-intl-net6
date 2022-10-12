using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemPermissionCore.Commands;

public class SystemPermissionCreateCommand : SystemPermission, IRequest<ISingleResult<Entity>>
{
}