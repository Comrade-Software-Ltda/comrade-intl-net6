using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.SystemUserServices.Dtos;

namespace Comrade.Application.Services.SystemUserServices.Commands;

public interface ISystemUserCommand : IService
{
    Task<ISingleResultDto<SystemUserDto>> Create(SystemUserCreateDto dto);
    Task<ISingleResultDto<SystemUserDto>> Edit(SystemUserEditDto dto);
    Task<ISingleResultDto<SystemUserDto>> Delete(int id);
}