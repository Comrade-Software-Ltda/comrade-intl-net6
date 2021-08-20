#region

using AutoMapper;
using Comrade.Application.Services.AuthenticationServices.Commands;
using Comrade.Persistence.DataAccess;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class AuthenticationInjectionService
{
    private readonly UcAuthenticationInjection _ucAuthenticationInjection = new();

    public AuthenticationCommand GetAuthenticationCommand(ComradeContext context,
        IMapper mapper)
    {
        var getUcUpdatePassword =
            _ucAuthenticationInjection.GetUcUpdatePassword(context);
        var getUcForgotPassword =
            _ucAuthenticationInjection.GetUcForgotPassword(context);
        var getUcValidateLogin =
            _ucAuthenticationInjection.GetUcValidateLogin(context);

        var authenticationService = new AuthenticationCommand(getUcUpdatePassword,
            getUcValidateLogin, getUcForgotPassword, mapper);
        return authenticationService;
    }
}
