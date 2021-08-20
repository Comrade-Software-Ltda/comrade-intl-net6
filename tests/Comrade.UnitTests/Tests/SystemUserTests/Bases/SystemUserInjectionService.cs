#region

using AutoMapper;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Domain.Extensions;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;

#endregion

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases;

public sealed class SystemUserInjectionService
{
    public SystemUserCommand GetSystemUserCommand(ComradeContext context, IMapper mapper)
    {
        var uow = new UnitOfWork(context);
        var systemUserRepository = new SystemUserRepository(context);
        var passwordHasher = new PasswordHasher(new HashingOptions());

        var systemUserEditValidation =
            new SystemUserEditValidation(systemUserRepository);
        var systemUserDeleteValidation = new SystemUserDeleteValidation(systemUserRepository);

        var ucSystemUserCreate =
            new UcSystemUserCreate(systemUserRepository, passwordHasher,
                uow);
        var ucSystemUserDelete =
            new UcSystemUserDelete(systemUserRepository, systemUserDeleteValidation, uow);
        var ucSystemUserEdit =
            new UcSystemUserEdit(systemUserRepository, systemUserEditValidation, uow);

        return new SystemUserCommand(ucSystemUserEdit, ucSystemUserCreate,
            ucSystemUserDelete,
            mapper);
    }

    public SystemUserQuery GetSystemUserQuery(ComradeContext context, IMapper mapper)
    {
        var systemUserRepository = new SystemUserRepository(context);

        return new SystemUserQuery(systemUserRepository, mapper);
    }
}
