using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.SystemMenu.Contracts;

namespace Comrade.Application.Components.SystemMenu.Commands;

public interface ISystemMenuCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemMenuCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemMenuEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}
