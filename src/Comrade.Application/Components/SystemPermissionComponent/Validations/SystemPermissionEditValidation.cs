using Comrade.Application.Components.SystemPermissionComponent.Contracts;

namespace Comrade.Application.Components.SystemPermissionComponent.Validations;

public class SystemPermissionEditValidation : SystemPermissionValidation<SystemPermissionEditDto>
{
    public SystemPermissionEditValidation()
    {
        ValidateName();
        ValidateTag();
    }
}
