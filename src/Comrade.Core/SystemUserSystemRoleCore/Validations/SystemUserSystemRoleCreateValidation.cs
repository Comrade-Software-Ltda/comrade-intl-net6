using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemRoleCore.Validations;

public class SystemUserSystemRoleCreateValidation : ISystemUserSystemRoleCreateValidation
{
    public async Task<ISingleResult<Entity>> Execute(SystemUserSystemRole entity)
    {
        return new SingleResult<Entity>(entity);
    }
}