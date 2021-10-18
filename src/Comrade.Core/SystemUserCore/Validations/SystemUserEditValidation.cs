using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserEditValidation : EntityValidation<SystemUser>
{
    public SystemUserEditValidation(ISystemUserRepository repository)
        : base(repository)
    {
    }

    public async Task<ISingleResult<SystemUser>> Execute(SystemUser entity)
    {
        var recordExists = await RecordExists(entity.Id).ConfigureAwait(false);
        if (!recordExists.Success)
        {
            return recordExists;
        }

        return recordExists;
    }
}