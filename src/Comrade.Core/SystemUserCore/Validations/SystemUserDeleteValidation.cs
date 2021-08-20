#region

using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserDeleteValidation : EntityValidation<SystemUser>
{
    private readonly ISystemUserRepository _repository;

    public SystemUserDeleteValidation(ISystemUserRepository repository)
        : base(repository)
    {
        _repository = repository;
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
