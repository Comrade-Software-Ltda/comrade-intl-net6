using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneEditValidation(IAirplaneCodeUniqueValidation airplaneCodeUniqueValidation)
    : IAirplaneEditValidation
{
    public async Task<ISingleResult<Entity>> Execute(Airplane entity, Airplane? recordExists)
    {
        var registerSameCode =
            await airplaneCodeUniqueValidation.Execute(entity);
        if (!registerSameCode.Success)
        {
            return registerSameCode;
        }

        return new SingleResult<Entity>(recordExists);
    }
}
