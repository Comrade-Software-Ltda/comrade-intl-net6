using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneCreateValidation : IAirplaneCreateValidation
{
    private readonly IAirplaneCodeUniqueValidation _airplaneCodeUniqueValidation;

    public AirplaneCreateValidation(IAirplaneRepository repository,
        IAirplaneCodeUniqueValidation airplaneCodeUniqueValidation)
    {
        _airplaneCodeUniqueValidation = airplaneCodeUniqueValidation;
    }

    public async Task<ISingleResult<Entity>> Execute(Airplane entity)
    {
        var registerSameCode =
            await _airplaneCodeUniqueValidation.Execute(entity).ConfigureAwait(false);
        if (!registerSameCode.Success)
        {
            return registerSameCode;
        }

        return new SingleResult<Entity>(entity);
    }
}
