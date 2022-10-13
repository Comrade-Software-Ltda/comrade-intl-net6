using Comrade.Api.Controllers.V1.SystemRoleApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemRoleTests.Bases;

public static class SystemRoleInjectionController
{
    public static SystemRoleController GetSystemRoleController(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMediator mediator)
    {
        var mapper = MapperHelper.ConfigMapper();
        var command = SystemRoleInjectionService.GetSystemRoleCommand(context, mediator);
        var query = SystemRoleInjectionService.GetSystemRoleQuery(context, mongoDbContextFixture, mapper);
        return new SystemRoleController(command, query);
    }
}
