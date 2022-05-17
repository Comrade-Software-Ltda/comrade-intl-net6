using Comrade.Application.Components.SystemUserComponent.Dtos;

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