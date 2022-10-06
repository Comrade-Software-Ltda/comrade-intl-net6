using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserSystemPermissionCore.Validations;

public interface ISystemUserSystemPermissionManageValidation
{
    ISingleResult<Entity> Execute(SystemUser entity);
}