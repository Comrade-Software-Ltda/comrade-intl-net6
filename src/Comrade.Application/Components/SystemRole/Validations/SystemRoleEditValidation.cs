using Comrade.Application.Components.SystemRoleComponent.Contracts;

namespace Comrade.Application.Components.SystemRoleComponent.Validations;

public class SystemRoleEditValidation : SystemRoleValidation<SystemRoleEditDto>
{
    public SystemRoleEditValidation()
    {
        ValidateName();
    }
}
