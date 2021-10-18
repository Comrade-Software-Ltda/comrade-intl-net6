using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore;

public interface IUcAirplaneCreate
{
    Task<ISingleResult<Airplane>> Execute(Airplane entity);
}