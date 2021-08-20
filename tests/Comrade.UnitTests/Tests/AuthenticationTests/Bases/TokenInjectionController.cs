#region

using Comrade.Api.UseCases.V1.LoginApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;

#endregion

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class TokenInjectionController
{
    private readonly AuthenticationInjectionService _authenticationInjectionService = new();

    public TokenController GetTokenController(ComradeContext context)
    {
        var mapper = MapperHelper.ConfigMapper();

        var authenticationCommand =
            _authenticationInjectionService.GetAuthenticationCommand(context, mapper);

        return new TokenController(authenticationCommand);
    }
}
