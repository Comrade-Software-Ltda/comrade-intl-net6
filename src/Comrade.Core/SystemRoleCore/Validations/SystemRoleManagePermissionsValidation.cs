using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemRoleCore.Validations;

public class SystemRoleManagePermissionsValidation : ISystemRoleManagePermissionsValidation
{
    public ISingleResult<Entity> Execute(SystemRole entity)
    {
        return new SingleResult<Entity>(entity);
    }
}
