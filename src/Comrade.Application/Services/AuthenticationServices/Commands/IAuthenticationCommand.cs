using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Application.Services.SystemUserServices.Dtos;

namespace Comrade.Application.Services.AuthenticationServices.Commands;

public interface IAuthenticationCommand : IService
{
    Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
    Task<ISingleResultDto<SystemUserDto>> ForgotPassword(AuthenticationDto dto);
    Task<ISingleResultDto<SystemUserDto>> UpdatePassword(AuthenticationDto dto);
}