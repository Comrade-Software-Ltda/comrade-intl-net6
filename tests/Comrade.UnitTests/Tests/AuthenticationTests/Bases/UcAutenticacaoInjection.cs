using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Domain.Extensions;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public sealed class UcAuthenticationInjection
{
    public static UcUpdatePassword GetUcUpdatePassword(IMediator mediator)
    {
        return new UcUpdatePassword(mediator);
    }

    public static UcForgotPassword GetUcForgotPassword(IMediator mediator)
    {
        return new UcForgotPassword(mediator);
    }

    public static UcValidateLogin GetUcValidateLogin(ComradeContext context)
    {
        var myConfiguration = new Dictionary<string, string>
        {
            { "JWT:Key", "afsdkjasjflxswafsdklk434orqiwup3457u-34oewir4irroqwiffv48mfs" }
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

        var systemUserCoreRepository = new SystemUserRepository(context);
        var ucGenerateToken = new UcGenerateToken(configuration);

        var passwordHasher = new PasswordHasher(new HashingOptions());

        var systemUserPasswordValidation =
            new SystemUserPasswordValidation(systemUserCoreRepository, passwordHasher);

        var ucGenerateTokenLogin =
            new UcValidateLogin
            (
                systemUserPasswordValidation,
                ucGenerateToken
            );
        return ucGenerateTokenLogin;
    }
}