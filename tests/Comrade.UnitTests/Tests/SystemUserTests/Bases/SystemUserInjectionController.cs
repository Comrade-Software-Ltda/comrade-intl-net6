#region

using Comrade.Api.UseCases.V2.SystemUserApi;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;

#endregion

namespace Comrade.UnitTests.Tests.SystemUserTests.Bases;

public class SystemUserInjectionController
{

    public static SystemUserController GetSystemUserController(ComradeContext context)
    {
        var mapper = MapperHelper.ConfigMapper();
        var systemUserCommand =
            SystemUserInjectionService.GetSystemUserCommand(context, mapper);
        var systemUserQuery = SystemUserInjectionService.GetSystemUserQuery(context, mapper);

        return new SystemUserController(systemUserCommand, systemUserQuery, mapper);
    }
}
