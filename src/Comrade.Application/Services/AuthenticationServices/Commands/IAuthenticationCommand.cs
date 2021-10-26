using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AuthenticationServices.Dtos;

namespace Comrade.Application.Services.AuthenticationServices.Commands;

public interface IAuthenticationCommand
{
    Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto);
}