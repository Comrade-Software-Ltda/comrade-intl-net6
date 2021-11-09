using Comrade.Core.AirplaneCore.Commands;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.AirplaneCore;

public interface IUcAirplaneCreate
{
    Task<ISingleResult<Entity>> Execute(AirplaneCreateCommand entity);
}