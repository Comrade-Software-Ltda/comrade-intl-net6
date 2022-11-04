using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleTagUniqueValidation : ISystemRoleTagUniqueValidation
{
    private readonly ISystemRoleRepository _repository;

    public SystemRoleTagUniqueValidation(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemRole entity)
    {
        var result = await _repository.UniqueTagValidation(entity.Tag).ConfigureAwait(false);
        return result.Success
            ? new SingleResult<Entity>(entity)
            : new SingleResult<Entity>(result.Code, result.Message);
    }
}
