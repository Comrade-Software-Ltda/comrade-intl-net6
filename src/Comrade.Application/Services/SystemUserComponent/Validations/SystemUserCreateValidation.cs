using Comrade.Application.Services.SystemUserComponent.Dtos;

namespace Comrade.Application.Services.SystemUserComponent.Validations;

public class SystemUserCreateValidation : SystemUserValidation<SystemUserCreateDto>
{
    public SystemUserCreateValidation()
    {
        ValidateName();
        ValidateEmail();
        ValidateRegistration();
    }
}