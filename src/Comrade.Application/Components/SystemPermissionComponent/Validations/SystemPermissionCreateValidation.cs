using Comrade.Application.Components.SystemPermissionComponent.Contracts;

namespace Comrade.Application.Components.SystemPermissionComponent.Validations;

public class SystemPermissionCreateValidation : SystemPermissionValidation<SystemPermissionCreateDto>
{
    public SystemPermissionCreateValidation()
    {
        ValidateName();
        ValidateTag();
    }
}
