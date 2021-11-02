using Comrade.Core.SecurityCore.UseCases;
using Comrade.Core.SecurityCore.Validation;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;

namespace Comrade.UnitTests.Tests.AuthenticationTests.Bases;

public sealed class UcAuthenticationInjection
{
    public static UcUpdatePassword GetUcUpdatePassword(ComradeContext context)
    {
        var uow = new UnitOfWork(context);
        var systemUserCoreRepository = new SystemUserRepository(context);

        var systemUserCoreEditValidation =
            new SystemUserEditValidation(
            );
        var passwordHasher = new PasswordHasher(new HashingOptions());

        return new UcUpdatePassword(systemUserCoreRepository,
            systemUserCoreEditValidation, passwordHasher, uow);
    }

    public static UcForgotPassword GetUcForgotPassword(ComradeContext context)
    {
        var uow = new UnitOfWork(context);
        var systemUserCoreRepository = new SystemUserRepository(context);
        var systemUserEditValidation = new SystemUserEditValidation();
        var systemUserForgotPasswordValidation =
            new SystemUserForgotPasswordValidation(
                systemUserEditValidation
            );
        var passwordHasher = new PasswordHasher(new HashingOptions());

        return new UcForgotPassword(systemUserCoreRepository,
            systemUserForgotPasswordValidation,
            passwordHasher, uow);
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