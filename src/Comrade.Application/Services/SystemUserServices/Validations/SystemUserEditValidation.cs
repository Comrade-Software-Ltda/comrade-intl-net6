#region

using Comrade.Application.Services.SystemUserServices.Dtos;

#endregion

namespace Comrade.Application.Services.SystemUserServices.Validations;

public class SystemUserEditValidation : SystemUserValidation<SystemUserEditDto>
{
    public SystemUserEditValidation()
    {
        ValidateId();
        ValidateName();
        ValidateEmail();
        PasswordValidation();
        ValidateRegistration();
    }
}
