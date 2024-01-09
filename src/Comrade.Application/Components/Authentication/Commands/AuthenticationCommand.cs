using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.Authentication.Contracts;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.Commands;

namespace Comrade.Application.Components.Authentication.Commands;

public class AuthenticationCommand(
    IUcUpdatePassword updatePassword,
    IUcValidateLogin validateLogin,
    IUcForgotPassword forgotPassword,
    IMapper mapper)
    : IAuthenticationCommand
{
    public async Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto)
    {
        var result = await validateLogin.Execute(dto.Key, dto.Password)
            ;

        if (!result.Success)
        {
            return new SingleResultDto<UserDto>(result);
        }

        var token = new UserDto
        {
            Token = result.TokenUser!.Token
        };

        return new SingleResultDto<UserDto>(token);
    }

    public async Task<ISingleResultDto<EntityDto>> ForgotPassword(AuthenticationDto dto)
    {
        var mappedObject = mapper.Map<ForgotPasswordCommand>(dto);
        var result = await forgotPassword.Execute(mappedObject);
        var resultDto = new SingleResultDto<EntityDto>(result);
        return resultDto;
    }

    public async Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto)
    {
        var mappedObject = mapper.Map<UpdatePasswordCommand>(dto);
        var result = await updatePassword.Execute(mappedObject);
        var resultDto = new SingleResultDto<EntityDto>(result);
        return resultDto;
    }
}
