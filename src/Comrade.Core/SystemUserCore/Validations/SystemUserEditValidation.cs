#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserEditValidation : EntityValidation<SystemUser>
{
    private readonly ISystemUserRepository _repository;

    public SystemUserEditValidation(ISystemUserRepository repository)
        : base(repository)
    {
        _repository = repository;
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
