#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore;

public interface IUcAirplaneCreate
{
    Task<ISingleResult<Airplane>> Execute(Airplane entity);
}