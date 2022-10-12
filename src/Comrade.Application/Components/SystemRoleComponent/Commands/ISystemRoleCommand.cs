using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemRoleComponent.Contracts;

namespace Comrade.Application.Components.SystemRoleComponent.Commands;

public interface ISystemRoleCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemRoleCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemRoleEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}
