using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore.Validations;

public class SystemMenuUniqueValidation : ISystemMenuUniqueValidation
{
    private readonly ISystemMenuRepository _repository;

    public SystemMenuUniqueValidation(ISystemMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemMenu entity)
    {
        var registerSameCode =
            await _repository.UniqueValidation(entity)
                .ConfigureAwait(false);

        return !registerSameCode.Success ? registerSameCode : new SingleResult<Entity>(entity);
    }
}
