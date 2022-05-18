using Comrade.Application.Components.SystemUserComponent.Contracts;

namespace Comrade.Application.Components.SystemUserComponent.Validations;

public class SystemUserEditValidation : SystemUserValidation<SystemUserEditDto>
{
    public SystemUserEditValidation()
    {
        ValidateName();
        ValidateEmail();
        ValidateRegistration();
    }
}