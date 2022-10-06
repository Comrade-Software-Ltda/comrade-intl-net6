using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemPermissionComponent.Contracts;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;

namespace Comrade.Application.Components.SystemUserSystemPermissionComponent.Commands
{
    public interface ISystemUserSystemPermissionCommand
    {
        Task<ISingleResultDto<EntityDto>> Manage(SystemUserSystemPermissionManageDto dto);
    }
}
