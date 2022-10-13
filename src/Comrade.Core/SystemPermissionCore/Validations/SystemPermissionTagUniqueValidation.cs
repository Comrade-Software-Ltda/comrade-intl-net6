using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionTagUniqueValidation : ISystemPermissionTagUniqueValidation
{
    private readonly ISystemPermissionRepository _repository;

    public SystemPermissionTagUniqueValidation(ISystemPermissionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemPermission entity)
    {
        var result = await _repository.TagUniqueValidation(entity.Tag).ConfigureAwait(false);

        return result.Success
            ? new SingleResult<Entity>(entity)
            : new SingleResult<Entity>(result.Code, result.Message);
    }
}
