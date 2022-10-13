using Comrade.Application.Components.SystemMenuComponent.Contracts;

namespace Comrade.Application.Components.SystemMenuComponent.Validations;

public class SystemMenuEditValidation : SystemMenuValidation<SystemMenuEditDto>
{
    public SystemMenuEditValidation()
    {
        ValidateTitle();
        ValidateDescription();
        ValidateRoute();
    }
}
