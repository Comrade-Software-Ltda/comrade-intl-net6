using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.SystemMenuServices.Dtos;

namespace Comrade.Application.Services.SystemMenuServices.Commands;

public interface ISystemMenuCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemMenuCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemMenuEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}