using Comrade.Api.UseCases.V1;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using Comrade.UnitTests.Tests.SystemUserTests.Bases;
using System;

namespace Comrade.IntegrationTests.Tests.LookupIntegrationTests;

public static class CommonControllerInjection
{
    public static CommonController GetCommonController(ComradeContext context,
        MongoDbContext mongoDbContextFixture, IServiceProvider serviceProvider)
    {
        var mapper = MapperHelper.ConfigMapper();
        var systemUserQuery =
            SystemUserInjectionService.GetSystemUserQuery(context, mongoDbContextFixture, mapper);
        var commonController =
            new CommonController(serviceProvider, systemUserQuery);
        return commonController;
    }
}