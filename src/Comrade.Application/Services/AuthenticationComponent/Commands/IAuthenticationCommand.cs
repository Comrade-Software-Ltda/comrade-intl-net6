using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AuthenticationComponent.Dtos;

namespace Comrade.Application.Services.AuthenticationComponent.Commands;

public interface IAuthenticationCommand
{
    Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto);
}