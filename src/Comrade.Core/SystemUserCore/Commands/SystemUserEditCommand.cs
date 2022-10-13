using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SystemUserCore.Commands;

public class SystemUserEditCommand : SystemUser, IRequest<ISingleResult<Entity>>
{
}
