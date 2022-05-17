using AutoMapper;
using Comrade.Application.Components.AuthenticationComponent.Commands;
using Comrade.Persistence.DataAccess;
using MediatR;

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class AuthenticationInjectionService
{
    public static AuthenticationCommand GetAuthenticationCommand(ComradeContext context,
        IMapper mapper, IMediator mediator)
    {
        var getUcUpdatePassword =
            UcAuthenticationInjection.GetUcUpdatePassword(mediator);
        var getUcForgotPassword =
            UcAuthenticationInjection.GetUcForgotPassword(mediator);
        var getUcValidateLogin =
            UcAuthenticationInjection.GetUcValidateLogin(context, mediator);

        var authenticationService = new AuthenticationCommand(getUcUpdatePassword,
            getUcValidateLogin, getUcForgotPassword, mapper);
        return authenticationService;
    }
}