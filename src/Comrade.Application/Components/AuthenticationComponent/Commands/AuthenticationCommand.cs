using AutoMapper;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Components.AuthenticationComponent.Contracts;
using Comrade.Core.SecurityCore;
using Comrade.Core.SecurityCore.Commands;

namespace Comrade.Application.Components.AuthenticationComponent.Commands;

public class AuthenticationCommand : IAuthenticationCommand
{
    private readonly IUcForgotPassword _forgotPassword;
    private readonly IMapper _mapper;
    private readonly IUcUpdatePassword _updatePassword;
    private readonly IUcValidateLogin _validateLogin;


    public AuthenticationCommand(IUcUpdatePassword updatePassword,
        IUcValidateLogin validateLogin,
        IUcForgotPassword forgotPassword,
        IMapper mapper)
    {
        _updatePassword = updatePassword;
        _forgotPassword = forgotPassword;
        _validateLogin = validateLogin;
        _mapper = mapper;
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
        var mappedObject = _mapper.Map<ForgotPasswordCommand>(dto);
        var result = await _forgotPassword.Execute(mappedObject).ConfigureAwait(false);
        var resultDto = new SingleResultDto<EntityDto>(result);
        return resultDto;
    }

    public async Task<ISingleResultDto<EntityDto>> UpdatePassword(AuthenticationDto dto)
    {
        var mappedObject = _mapper.Map<UpdatePasswordCommand>(dto);
        var result = await _updatePassword.Execute(mappedObject).ConfigureAwait(false);
        var resultDto = new SingleResultDto<EntityDto>(result);
        return resultDto;
    }
}