using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserCore.Commands;

public class SystemUserCreateCommand : SystemUser, IRequest<ISingleResult<Entity>>
{
}
