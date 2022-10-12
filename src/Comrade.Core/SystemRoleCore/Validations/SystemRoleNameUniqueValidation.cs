using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleNameUniqueValidation : ISystemRoleNameUniqueValidation
{
    private readonly ISystemRoleRepository _repository;

    public SystemRoleNameUniqueValidation(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var result = await _repository.NameUniqueValidation(entity.Name).ConfigureAwait(false);
        return result.Success
            ? new SingleResult<Entity>(entity)
            : new SingleResult<Entity>(result.Code, result.Message);
    }
}
