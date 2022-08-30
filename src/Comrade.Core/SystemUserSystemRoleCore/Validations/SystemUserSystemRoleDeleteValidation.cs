using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemRoleCore.Validations;

public class SystemUserSystemRoleDeleteValidation : ISystemUserSystemRoleDeleteValidation
{
    public ISingleResult<Entity> Execute(SystemUserSystemRole? recordExists)
    {
        return new SingleResult<Entity>(recordExists);
    }
}