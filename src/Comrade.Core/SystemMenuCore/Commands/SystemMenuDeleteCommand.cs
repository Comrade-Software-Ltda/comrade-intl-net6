using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemMenuCore.Commands;

public class SystemMenuDeleteCommand : SystemMenu, IRequest<ISingleResult<Entity>>
{
}