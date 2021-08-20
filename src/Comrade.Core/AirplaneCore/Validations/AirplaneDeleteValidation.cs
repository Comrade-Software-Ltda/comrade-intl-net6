#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneDeleteValidation : EntityValidation<Airplane>
{
    private readonly IAirplaneRepository _repository;

    public AirplaneDeleteValidation(IAirplaneRepository repository)
        : base(repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Airplane>> Execute(int id)
    {
        var recordExists = await RecordExists(id).ConfigureAwait(false);
        if (!recordExists.Success)
        {
            return recordExists;
        }

        return recordExists;
    }
}
