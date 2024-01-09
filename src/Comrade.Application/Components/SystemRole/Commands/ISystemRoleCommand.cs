using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRole.Contracts;

namespace Comrade.Application.Components.SystemRole.Commands;

public interface ISystemRoleCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemRoleCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemRoleEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
    Task<ISingleResultDto<EntityDto>> ManagePermissions(SystemRoleManagePermissionsDto dto);
}
