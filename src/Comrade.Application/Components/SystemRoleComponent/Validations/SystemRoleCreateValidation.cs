using Comrade.Application.Components.SystemRoleComponent.Contracts;

namespace Comrade.Application.Components.SystemRoleComponent.Validations;

public class SystemRoleCreateValidation : SystemRoleValidation<SystemRoleCreateDto>
{
    public SystemRoleCreateValidation()
    {
        ValidateName();
    }
}
