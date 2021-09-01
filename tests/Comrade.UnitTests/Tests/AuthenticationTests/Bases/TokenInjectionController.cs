#region

using Comrade.Api.UseCases.V1.LoginApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class TokenInjectionController
{
    public static TokenController GetTokenController(ComradeContext context)
    {
        var mapper = MapperHelper.ConfigMapper();

        var authenticationCommand =
            AuthenticationInjectionService.GetAuthenticationCommand(context, mapper);

        return new TokenController(authenticationCommand);
    }
}
