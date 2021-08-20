#region

using Comrade.Application.Services.SystemUserServices.Dtos;

#endregion

namespace Comrade.Application.Services.SystemUserServices.Validations;

public class SystemUserCreateValidation : SystemUserValidation<SystemUserCreateDto>
{
    public SystemUserCreateValidation()
    {
        ValidateName();
        ValidateEmail();
        PasswordValidation();
        ValidateRegistration();
    }
}
