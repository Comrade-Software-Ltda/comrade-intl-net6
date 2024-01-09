using Comrade.Application.Components.SystemRole.Contracts;

namespace Comrade.Application.Components.SystemRole.Validations;

public class SystemRoleEditValidation : SystemRoleValidation<SystemRoleEditDto>
{
    public SystemRoleEditValidation()
    {
        ValidateName();
    }
}
