using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore;

public interface IUcAirplaneEdit
{
    Task<ISingleResult<Airplane>> Execute(Airplane entity);
}