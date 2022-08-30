using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemRoleCore.Validations;

public interface ISystemUserSystemRoleDeleteValidation
{
    ISingleResult<Entity> Execute(SystemUserSystemRole? recordExists);
}