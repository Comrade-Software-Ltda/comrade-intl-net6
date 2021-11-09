using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.AirplaneCore.Commands;

public class AirplaneDeleteCommand : Airplane, IRequest<ISingleResult<Entity>>
{
}