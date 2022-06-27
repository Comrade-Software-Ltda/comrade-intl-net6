using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.AuthenticationComponent.Contracts;

namespace Comrade.Application.Components.AuthenticationComponent.Commands;

public interface IAuthenticationCommand
{
    Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto);
    Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto);
}