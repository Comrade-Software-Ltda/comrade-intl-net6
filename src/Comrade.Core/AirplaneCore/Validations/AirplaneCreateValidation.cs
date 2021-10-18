using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneCreateValidation : EntityValidation<Airplane>
{
    private readonly AirplaneValidateSameCode _airplaneValidateSameCode;

    public AirplaneCreateValidation(IAirplaneRepository repository,
        AirplaneValidateSameCode airplaneValidateSameCode)
        : base(repository)
    {
        _airplaneValidateSameCode = airplaneValidateSameCode;
    }

    public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
    {
        return await _airplaneValidateSameCode.Execute(entity).ConfigureAwait(false);
    }
}