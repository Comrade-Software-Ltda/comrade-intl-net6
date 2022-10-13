using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneCodeUniqueValidation : IAirplaneCodeUniqueValidation
{
    private readonly IAirplaneRepository _repository;

    public AirplaneCodeUniqueValidation(IAirplaneRepository repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(Airplane entity)
    {
        var result = await _repository.CodeUniqueValidation(entity.Id, entity.Code)
            .ConfigureAwait(false);

        return new SingleResult<Entity>(entity);
    }
}
