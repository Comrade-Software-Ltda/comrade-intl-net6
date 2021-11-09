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

    public static UcValidateLogin GetUcValidateLogin(ComradeContext context, IMediator mediator)
    {
        var systemUserCoreRepository = new SystemUserRepository(context);
        var ucGenerateToken = new UcGenerateToken(mediator);

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