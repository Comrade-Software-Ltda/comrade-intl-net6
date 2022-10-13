using AutoMapper;
using Comrade.Application.Components.SystemRoleComponent.Commands;
using Comrade.Application.Components.SystemRoleComponent.Queries;
using Comrade.Core.SystemRoleCore.UseCases;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemRoleTests.Bases;

public sealed class SystemRoleInjectionService
{
    public static SystemRoleCommand GetSystemRoleCommand(ComradeContext context, IMediator mediator)
    {
        var ucDelete = new UcSystemRoleDelete(mediator);
        return new SystemRoleCommand(ucDelete, mediator);
    }

    public static SystemRoleQuery GetSystemRoleQuery(ComradeContext context, MongoDbContext mongoDbContextFixture,
        IMapper mapper)
    {
        var repository = new SystemRoleRepository(context);
        return new SystemRoleQuery(repository, mongoDbContextFixture, mapper);
    }
}
