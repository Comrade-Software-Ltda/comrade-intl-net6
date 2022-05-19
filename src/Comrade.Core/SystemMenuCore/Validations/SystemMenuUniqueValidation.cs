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
        var result = await _repository.CodeUniqueValidation(entity.Id, entity.Text)
            .ConfigureAwait(false);

        return new SingleResult<Entity>(entity);
    }
}