using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemMenuCore.Validations;

public class SystemMenuCreateValidation : ISystemMenuCreateValidation
{
    private readonly SystemMenuValidateSameCode _systemMenuValidateSameCode;

    public SystemMenuCreateValidation(ISystemMenuRepository repository,
        SystemMenuValidateSameCode systemMenuValidateSameCode)
    {
        _systemMenuValidateSameCode = systemMenuValidateSameCode;
    }

    public async Task<ISingleResult<Entity>> Execute(SystemMenu entity)
    {
        var registerSameCode =
            await _systemMenuValidateSameCode.Execute(entity).ConfigureAwait(false);
        if (!registerSameCode.Success)
        {
            return registerSameCode;
        }

        return new SingleResult<Entity>(entity);
    }
}