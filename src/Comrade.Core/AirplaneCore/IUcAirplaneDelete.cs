#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore;

public interface IUcAirplaneDelete
{
    Task<ISingleResult<Airplane>> Execute(int id);
}
