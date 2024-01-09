using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionTagUniqueValidation(ISystemPermissionRepository repository)
    : ISystemPermissionTagUniqueValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemPermission entity)
    {
        var result = await repository.TagUniqueValidation(entity.Tag);

        return result.Success
            ? new SingleResult<Entity>(entity)
            : new SingleResult<Entity>(result.Code, result.Message);
    }
}
