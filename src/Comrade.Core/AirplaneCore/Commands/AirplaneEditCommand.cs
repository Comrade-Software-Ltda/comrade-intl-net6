using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;
using MediatR;

namespace Comrade.Core.AirplaneCore.Commands;

public class AirplaneEditCommand : Airplane, IRequest<ISingleResult<Entity>>
{
}