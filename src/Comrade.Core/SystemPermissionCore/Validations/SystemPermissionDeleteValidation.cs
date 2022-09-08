using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemPermissionCore.Validations;

public class SystemPermissionDeleteValidation : ISystemPermissionDeleteValidation
{
    public ISingleResult<Entity> Execute(SystemPermission? recordExists)
    {
        return new SingleResult<Entity>(recordExists);
    }
}