using Comrade.Application.Components.SystemRole.Contracts;

namespace Comrade.Application.Components.SystemRole.Validations;

public class SystemRoleCreateValidation : SystemRoleValidation<SystemRoleCreateDto>
{
    public SystemRoleCreateValidation()
    {
        ValidateName();
        ValidateTag();
    }
}
