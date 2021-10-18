using Comrade.Application.Services.SystemUserServices.Dtos;

namespace Comrade.Application.Services.SystemUserServices.Validations;

public class SystemUserDeleteValidation : SystemUserValidation<SystemUserDto>
{
    public SystemUserDeleteValidation()
    {
        ValidateId();
    }
}