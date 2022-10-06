using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemRoleCore.Validations;

public interface ISystemUserSystemRoleManageValidation
{
    ISingleResult<Entity> Execute(SystemUser entity);
}