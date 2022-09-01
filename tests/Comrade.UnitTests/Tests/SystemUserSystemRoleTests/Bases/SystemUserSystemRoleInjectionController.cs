using Comrade.Api.Controllers.V1.SystemUserSystemRoleApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemUserSystemRoleTests.Bases;

public class SystemUserSystemRoleInjectionController
{
    public static SystemUserSystemRoleController GetSystemUserSystemRoleController(ComradeContext context, MongoDbContext mongoDbContextFixture, IMediator mediator)
    {
        var mapper  = MapperHelper.ConfigMapper();
        var command = SystemUserSystemRoleInjectionService.GetSystemUserSystemRoleCommand(context, mediator);
        var query   = SystemUserSystemRoleInjectionService.GetSystemUserSystemRoleQuery(context, mongoDbContextFixture, mapper);
        return new SystemUserSystemRoleController(command, query);
    }
}