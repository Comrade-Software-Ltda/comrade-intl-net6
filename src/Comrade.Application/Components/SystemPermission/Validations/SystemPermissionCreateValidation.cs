using Comrade.Application.Components.SystemPermission.Contracts;

namespace Comrade.Application.Components.SystemPermission.Validations;

public class SystemPermissionCreateValidation : SystemPermissionValidation<SystemPermissionCreateDto>
{
    public SystemPermissionCreateValidation()
    {
        ValidateName();
        ValidateTag();
    }
}
