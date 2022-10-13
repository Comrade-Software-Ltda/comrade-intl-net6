using Comrade.Api.Controllers.V1.SystemPermissionApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemPermissionTests.Bases;

public static class SystemPermissionInjectionController
{
    public static SystemPermissionController GetSystemPermissionController(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMediator mediator)
    {
        var mapper = MapperHelper.ConfigMapper();
        var command = SystemPermissionInjectionService.GetSystemPermissionCommand(context, mediator);
        var query = SystemPermissionInjectionService.GetSystemPermissionQuery(context, mongoDbContextFixture, mapper);
        return new SystemPermissionController(command, query);
    }
}
