using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

namespace Comrade.Core.AirplaneCore.Validations;

public class AirplaneDeleteValidation : EntityValidation<Airplane>
{
    public AirplaneDeleteValidation(IAirplaneRepository repository)
        : base(repository)
    {
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