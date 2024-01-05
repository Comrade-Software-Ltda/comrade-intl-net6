using Comrade.Application.Components.SystemUser.Contracts;

namespace Comrade.Application.Components.SystemUser.Validations;

public class SystemUserCreateValidation : SystemUserValidation<SystemUserCreateDto>
{
    public SystemUserCreateValidation()
    {
        ValidateName();
        ValidateEmail();
        ValidateRegistration();
    }
}
