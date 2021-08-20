#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneEditValidation : EntityValidation<Airplane>
{
    private readonly AirplaneValidateSameCode _airplaneValidateSameCode;
    private readonly IAirplaneRepository _repository;

    public AirplaneEditValidation(IAirplaneRepository repository,
        AirplaneValidateSameCode airplaneValidateSameCode)
        : base(repository)
    {
        _repository = repository;
        _airplaneValidateSameCode = airplaneValidateSameCode;
    }

    public async Task<ISingleResult<Airplane>> Execute(Airplane entity)
    {
        var recordExists = await RecordExists(entity.Id).ConfigureAwait(false);
        if (!recordExists.Success)
        {
            return recordExists;
        }

        var registerSameCode =
            await _airplaneValidateSameCode.Execute(entity).ConfigureAwait(false);
        if (!registerSameCode.Success)
        {
            return registerSameCode;
        }

        return recordExists;
    }
}
