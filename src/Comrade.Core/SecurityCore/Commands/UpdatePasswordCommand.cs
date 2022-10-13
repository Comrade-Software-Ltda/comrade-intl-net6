using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.SecurityCore.Commands;

public class UpdatePasswordCommand : SystemUser, IRequest<ISingleResult<Entity>>
{
}
