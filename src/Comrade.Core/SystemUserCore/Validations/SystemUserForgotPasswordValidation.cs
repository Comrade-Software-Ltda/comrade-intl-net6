using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserForgotPasswordValidation(ISystemUserEditValidation systemUserEditValidation)
    : ISystemUserForgotPasswordValidation
{
    public ISingleResult<Entity> Execute(SystemUser entity, SystemUser? recordExists)
    {
        var systemUserEditValidationResult =
            systemUserEditValidation.Execute(entity, recordExists);
        if (!systemUserEditValidationResult.Success)
        {
            return systemUserEditValidationResult;
        }

        return new SingleResult<Entity>(recordExists);
    }
}
