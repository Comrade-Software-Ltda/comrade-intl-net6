using AutoMapper;
using Comrade.Application.Components.SystemPermissionComponent.Commands;
using Comrade.Application.Components.SystemPermissionComponent.Queries;
using Comrade.Core.SystemPermissionCore.UseCases;
using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemPermissionTests.Bases;

public sealed class SystemPermissionInjectionService
{
    public static SystemPermissionCommand GetSystemPermissionCommand(ComradeContext context, IMediator mediator)
    {
        var ucDelete = new UcSystemPermissionDelete(mediator);
        return new SystemPermissionCommand(ucDelete, mediator);
    }

    public static SystemPermissionQuery GetSystemPermissionQuery(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMapper mapper)
    {
        var repository = new SystemPermissionRepository(context);
        return new SystemPermissionQuery(repository, mongoDbContextFixture, mapper);
    }
}
