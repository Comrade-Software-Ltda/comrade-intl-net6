using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Services.AuthenticationServices.Dtos;
using Comrade.Core.SecurityCore;
using Comrade.Domain.Models;

namespace Comrade.Application.Services.AuthenticationServices.Commands;

public class AuthenticationCommand : Service, IAuthenticationCommand
{
    private readonly IUcForgotPassword _forgotPassword;
    private readonly IUcUpdatePassword _updatePassword;
    private readonly IUcValidateLogin _validateLogin;

    public AuthenticationCommand(IUcUpdatePassword updatePassword,
        IUcValidateLogin validateLogin,
        IUcForgotPassword forgotPassword,
        IMapper mapper) :
        base(mapper)
    {
        _updatePassword = updatePassword;
        _forgotPassword = forgotPassword;
        _validateLogin = validateLogin;
    }

    public async Task<ISingleResultDto<UserDto>> GenerateToken(AuthenticationDto dto)
    {
        var result = await _validateLogin.Execute(dto.Key, dto.Password)
            .ConfigureAwait(false);

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
        var mappedObject = Mapper.Map<SystemUser>(dto);

        var result = await _forgotPassword.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }

    public async Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto)
    {
        var mappedObject = Mapper.Map<SystemUser>(dto);

        var result = await _updatePassword.Execute(mappedObject).ConfigureAwait(false);

        var resultDto = new SingleResultDto<EntityDto>(result);

        return resultDto;
    }
}