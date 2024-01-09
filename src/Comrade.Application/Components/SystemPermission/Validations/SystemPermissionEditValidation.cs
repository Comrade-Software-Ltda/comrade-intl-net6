using Comrade.Application.Components.SystemPermission.Contracts;

namespace Comrade.Application.Components.SystemPermission.Validations;

public class SystemPermissionEditValidation : SystemPermissionValidation<SystemPermissionEditDto>
{
    public SystemPermissionEditValidation()
    {
        ValidateName();
        ValidateTag();
    }
}
