using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemRoleCore.Validations;

public class SystemUserSystemRoleManageValidation : ISystemUserSystemRoleManageValidation
{
    public ISingleResult<Entity> Execute(SystemUser entity)
    {
        return new SingleResult<Entity>(entity);
    }
}