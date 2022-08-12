using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemMenuComponent.Contracts;

namespace Comrade.Application.Components.SystemMenuComponent.Commands;

public interface ISystemMenuCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemMenuCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemMenuEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}