using AutoMapper;
using Comrade.Application.Services.AuthenticationServices.Commands;
using Comrade.Persistence.DataAccess;

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class AuthenticationInjectionService
{
    public static AuthenticationCommand GetAuthenticationCommand(ComradeContext context,
        IMapper mapper)
    {
        var getUcUpdatePassword =
            UcAuthenticationInjection.GetUcUpdatePassword(context);
        var getUcForgotPassword =
            UcAuthenticationInjection.GetUcForgotPassword(context);
        var getUcValidateLogin =
            UcAuthenticationInjection.GetUcValidateLogin(context);

        var authenticationService = new AuthenticationCommand(getUcUpdatePassword,
            getUcValidateLogin, getUcForgotPassword, mapper);
        return authenticationService;
    }
}