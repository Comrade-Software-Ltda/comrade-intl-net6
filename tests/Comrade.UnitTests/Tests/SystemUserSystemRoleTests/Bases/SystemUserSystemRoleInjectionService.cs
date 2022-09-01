using AutoMapper;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Commands;
using Comrade.Application.Components.SystemUserSystemRoleComponent.Queries;
using Comrade.Core.SystemUserSystemRoleCore.UseCases;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;

public sealed class SystemUserSystemRoleInjectionService
{
    public static SystemUserSystemRoleCommand GetSystemUserSystemRoleCommand(ComradeContext context, IMediator mediator)
    {
        var ucDelete = new UcSystemUserSystemRoleDelete(mediator);
        return new SystemUserSystemRoleCommand(ucDelete, mediator);
    }

    public static SystemUserSystemRoleQuery GetSystemUserSystemRoleQuery(ComradeContext context, MongoDbContext mongoDbContextFixture, IMapper mapper)
    {
        var repository = new SystemUserSystemRoleRepository(context);
        return new SystemUserSystemRoleQuery(repository, mongoDbContextFixture, mapper);
    }
}