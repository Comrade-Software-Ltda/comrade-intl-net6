using Comrade.Api.Controllers.V1.SystemMenuApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;

namespace Comrade.UnitTests.Tests.SystemMenuTests.Bases;

public class SystemMenuInjectionController
{
    public static SystemMenuController GetSystemMenuController(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IMediator mediator)
    {
        var mapper = MapperHelper.ConfigMapper();
        var logger = Mock.Of<ILogger<SystemMenuController>>();

        var systemMenuCommand =
            SystemMenuInjectionService.GetSystemMenuCommand(context, mediator);
        var systemMenuQuery =
            SystemMenuInjectionService.GetSystemMenuQuery(context!, mongoDbContextFixture, mapper);
        return new SystemMenuController(systemMenuCommand, systemMenuQuery, logger);
    }
}