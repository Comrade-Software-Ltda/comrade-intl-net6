using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.Authentication.Contracts;

namespace Comrade.Application.Components.Authentication.Commands;

public interface IAuthenticationCommand
{
    Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto);
}
