using Comrade.Application.Services.SystemUserComponent.Dtos;

namespace Comrade.Application.Services.SystemUserComponent.Validations;

public class SystemUserEditValidation : SystemUserValidation<SystemUserEditDto>
{
    public SystemUserEditValidation()
    {
        ValidateName();
        ValidateEmail();
        ValidateRegistration();
    }
}