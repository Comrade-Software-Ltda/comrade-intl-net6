using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;

namespace Comrade.Core.AirplaneCore;

public interface IUcAirplaneDelete
{
    Task<ISingleResult<Entity>> Execute(Guid id);
}