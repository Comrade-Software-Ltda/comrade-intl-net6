using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserDeleteValidation : EntityValidation<SystemUser>
{
    public SystemUserDeleteValidation(ISystemUserRepository repository)
        : base(repository)
    {
    }

    public async Task<ISingleResult<SystemUser>> Execute(int id)
    {
        var recordExists = await RecordExists(id).ConfigureAwait(false);
        if (!recordExists.Success)
        {
            return recordExists;
        }

        return recordExists;
    }
}