using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUser.Contracts;

namespace Comrade.Application.Components.SystemUser.Commands;

public interface ISystemUserCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
    Task<ISingleResultDto<EntityDto>> ManagePermissions(SystemUserManagePermissionsDto dto);
    Task<ISingleResultDto<EntityDto>> ManageRoles(SystemUserManageRolesDto dto);
}
