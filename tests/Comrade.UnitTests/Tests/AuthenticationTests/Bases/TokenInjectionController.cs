using Comrade.Api.Controllers.V1.LoginApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public class TokenInjectionController
{
    public static TokenController GetTokenController(ComradeContext context, IMediator mediator)
    {
        var mapper = MapperHelper.ConfigMapper();

        var authenticationCommand =
            AuthenticationInjectionService.GetAuthenticationCommand(context, mapper, mediator);

        return new TokenController(authenticationCommand);
    }
}
