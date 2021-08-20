#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneCreateValidation : EntityValidation<Airplane>
{
    private readonly AirplaneValidateSameCode _airplaneValidateSameCode;
    private readonly IAirplaneRepository _repository;

    public AirplaneCreateValidation(IAirplaneRepository repository,
        AirplaneValidateSameCode airplaneValidateSameCode)
        : base(repository)
    {
        _repository = repository;
        _airplaneValidateSameCode = airplaneValidateSameCode;
    }

    public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
    {
        return await _airplaneValidateSameCode.Execute(entity).ConfigureAwait(false);
    }
}
