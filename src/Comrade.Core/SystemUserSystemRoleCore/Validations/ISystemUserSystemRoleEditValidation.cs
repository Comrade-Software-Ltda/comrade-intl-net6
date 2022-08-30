using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemRoleCore.Validations;

public interface ISystemUserSystemRoleEditValidation
{
    Task<ISingleResult<Entity>> Execute(SystemUserSystemRole entity, SystemUserSystemRole? recordExists);
}