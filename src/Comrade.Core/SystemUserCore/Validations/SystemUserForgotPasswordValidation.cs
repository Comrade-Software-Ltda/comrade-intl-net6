using Comrade.Core.Bases.Interfaces;
using Comrade.Core.Bases.Results;
using Comrade.Core.Bases.Validations;
using Comrade.Domain.Models;

namespace Comrade.Core.SystemUserCore.Validations;

public class SystemUserForgotPasswordValidation : EntityValidation<SystemUser>
{
    private readonly SystemUserEditValidation _systemUserEditValidation;

    public SystemUserForgotPasswordValidation(ISystemUserRepository repository,
        SystemUserEditValidation systemUserEditValidation)
        : base(repository)
    {
        _systemUserEditValidation = systemUserEditValidation;
    }

    public ISingleResult<SystemUser> Execute(SystemUser entity)
    {
        var recordExists = _systemUserEditValidation.Execute(entity).Result;

        if (!recordExists.Success)
        {
            return new SingleResult<SystemUser>(1001, "TokenUser não exists");
        }


        return recordExists;
    }
}