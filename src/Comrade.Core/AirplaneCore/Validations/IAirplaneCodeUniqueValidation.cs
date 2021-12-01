using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public interface IAirplaneCodeUniqueValidation
{
    Task<ISingleResult<Entity>> Execute(Airplane entity);
}