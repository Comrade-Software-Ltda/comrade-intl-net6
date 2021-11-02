using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using MediatR;

namespace Comrade.Core.AirplaneCore.Commands;

public class AirplaneDeleteCommand : IRequest<ISingleResult<Entity>>
{
}