using AutoMapper;
using Comrade.Application.Services.SystemUserServices.Commands;
using Comrade.Application.Services.SystemUserServices.Queries;
using Comrade.Core.SystemUserCore.UseCases;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases;

public sealed class SystemUserInjectionService
{
    public static SystemUserCommand GetSystemUserCommand(ComradeContext context, IMediator mediator)
    {
        var ucSystemUserDelete =
            new UcSystemUserDelete(mediator);

        return new SystemUserCommand(ucSystemUserDelete, mediator);
    }

    public static SystemUserQuery GetSystemUserQuery(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMapper mapper)
    {
        var systemUserRepository = new SystemUserRepository(context);

        return new SystemUserQuery(systemUserRepository, mongoDbContextFixture, mapper);
    }
}