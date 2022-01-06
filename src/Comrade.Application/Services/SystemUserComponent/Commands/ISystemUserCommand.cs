using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.SystemUserComponent.Dtos;

namespace Comrade.Application.Services.SystemUserComponent.Commands;

public interface ISystemUserCommand
{
    Task<ISingleResultDto<EntityDto>> Create(SystemUserCreateDto dto);
    Task<ISingleResultDto<EntityDto>> Edit(SystemUserEditDto dto);
    Task<ISingleResultDto<EntityDto>> Delete(Guid id);
}