using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemPermissionComponent.Contracts;

namespace Comrade.Application.Components.SystemPermissionComponent.Commands;

public interface ISystemPermissionCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemPermissionCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemPermissionEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}
