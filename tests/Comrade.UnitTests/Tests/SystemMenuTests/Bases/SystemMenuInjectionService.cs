using AutoMapper;
using Comrade.Application.Components.SystemMenuComponent.Commands;
using Comrade.Application.Components.SystemMenuComponent.Queries;
using Comrade.Core.SystemMenuCore.UseCases;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemMenuTests.Bases;

public static class SystemMenuInjectionService
{
    public static SystemMenuCommand GetSystemMenuCommand(ComradeContext context,
        IMediator mediator)
    {
        var ucSystemMenuDelete =
            new UcSystemMenuDelete(mediator);

        return new SystemMenuCommand(ucSystemMenuDelete, mediator);
    }

    public static SystemMenuQuery GetSystemMenuQuery(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMapper mapper)
    {
        var systemMenuRepository = new SystemMenuRepository(context);

        return new SystemMenuQuery(systemMenuRepository, mongoDbContextFixture, mapper);
    }
}
