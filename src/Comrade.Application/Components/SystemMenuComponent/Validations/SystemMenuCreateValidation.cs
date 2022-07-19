using Comrade.Application.Components.SystemMenuComponent.Contracts;

namespace Comrade.Application.Components.SystemMenuComponent.Validations;

public class SystemMenuCreateValidation : SystemMenuValidation<SystemMenuCreateDto>
{
    public SystemMenuCreateValidation()
    {
        ValidateText();
        ValidateDescription();
        ValidateRoute();
    }
}