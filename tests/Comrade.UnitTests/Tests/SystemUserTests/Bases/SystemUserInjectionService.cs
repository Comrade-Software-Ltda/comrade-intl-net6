using AutoMapper;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Core.SystemUserCore.Validations;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases;

public sealed class SystemUserInjectionService
{
    public static SystemUserCommand GetSystemUserCommand(ComradeContext context, IMediator mediator)
    {
        var uow = new UnitOfWork(context);
        var systemUserRepository = new SystemUserRepository(context);
        var systemUserDeleteValidation = new SystemUserDeleteValidation(systemUserRepository);
        var ucSystemUserDelete =
            new UcSystemUserDelete(systemUserRepository, systemUserDeleteValidation, uow);

        return new SystemUserCommand(ucSystemUserDelete, mediator);
    }

    public static SystemUserQuery GetSystemUserQuery(ComradeContext context, IMapper mapper)
    {
        var systemUserRepository = new SystemUserRepository(context);

        return new SystemUserQuery(systemUserRepository, mapper);
    }
}