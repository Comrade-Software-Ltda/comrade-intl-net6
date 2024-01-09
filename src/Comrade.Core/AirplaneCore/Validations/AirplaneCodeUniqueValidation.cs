using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneCodeUniqueValidation(IAirplaneRepository repository) : IAirplaneCodeUniqueValidation
{
    public async Task<ISingleResult<Entity>> Execute(Airplane entity)
    {
        var result = await repository.CodeUniqueValidation(entity.Id, entity.Code)
            ;

        return new SingleResult<Entity>(entity);
    }
}
