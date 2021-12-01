using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneEditValidation : IAirplaneEditValidation
{
    private readonly IAirplaneCodeUniqueValidation _airplaneCodeUniqueValidation;

    public AirplaneEditValidation(IAirplaneCodeUniqueValidation airplaneCodeUniqueValidation)
    {
        _airplaneCodeUniqueValidation = airplaneCodeUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(Airplane entity, Airplane? recordExists)
    {
        var registerSameCode =
            await _airplaneCodeUniqueValidation.Execute(entity).ConfigureAwait(false);
        if (!registerSameCode.Success)
        {
            return registerSameCode;
        }

        return new SingleResult<Entity>(recordExists);
    }
}