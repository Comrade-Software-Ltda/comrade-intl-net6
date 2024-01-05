using Comrade.Application.Components.SystemMenu.Contracts;

namespace Comrade.Application.Components.SystemMenu.Validations;

public class SystemMenuEditValidation : SystemMenuValidation<SystemMenuEditDto>
{
    public SystemMenuEditValidation()
    {
        ValidateTitle();
        ValidateDescription();
        ValidateRoute();
    }
}
