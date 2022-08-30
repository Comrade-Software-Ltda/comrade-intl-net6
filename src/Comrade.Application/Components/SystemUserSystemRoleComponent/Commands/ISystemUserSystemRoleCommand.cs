using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Contracts;

namespace Comrade.Application.Components.SystemUserSystemRoleComponent.Commands;

public interface ISystemUserSystemRoleCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemUserSystemRoleCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemUserSystemRoleEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}