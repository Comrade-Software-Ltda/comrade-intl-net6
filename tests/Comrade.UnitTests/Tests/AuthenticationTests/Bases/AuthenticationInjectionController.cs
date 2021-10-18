using Comrade.Api.UseCases.V1.LoginApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class AuthenticationInjectionController
{
    public static AuthenticationController GetAuthenticationController(ComradeContext context)
    {
        var mapper = MapperHelper.ConfigMapper();

        var authenticationCommand =
            AuthenticationInjectionService.GetAuthenticationCommand(context, mapper);

        return new AuthenticationController(authenticationCommand);
    }
}