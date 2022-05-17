using Comrade.Application.Components.SystemUserComponent.Dtos;

namespace Comrade.Application.Components.SystemUserComponent.Validations;

public class SystemUserCreateValidation : SystemUserValidation<SystemUserCreateDto>
{
    public SystemUserCreateValidation()
    {
        ValidateName();
        ValidateEmail();
        ValidateRegistration();
    }
}