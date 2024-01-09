using Comrade.Application.Components.SystemUser.Contracts;

namespace Comrade.Application.Components.SystemUser.Validations;

public class SystemUserEditValidation : SystemUserValidation<SystemUserEditDto>
{
    public SystemUserEditValidation()
    {
        ValidateName();
        ValidateEmail();
        ValidateRegistration();
    }
}
