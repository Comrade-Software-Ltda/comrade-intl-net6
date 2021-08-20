#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneValidateSameCode : EntityValidation<Airplane>
{
    private readonly IAirplaneRepository _repository;

    public AirplaneValidateSameCode(IAirplaneRepository repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
    {
        var result = await _repository.ValidateSameCode(entity.Id, entity.Code)
            .ConfigureAwait(false);

        return result;
    }
}
