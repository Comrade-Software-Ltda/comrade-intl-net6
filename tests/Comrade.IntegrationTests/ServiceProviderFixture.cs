using System;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;

namespace Comrade.IntegrationTests
{
    public class ServiceProviderFixture : IDisposable
    {
        public void Dispose()
        {
        }

        public IServiceProvider InitiateConxtext(string contextName)
        {
            var serviceCollection = GetServiceCollection.Execute();


            serviceCollection.AddDbContext<ComradeContext>(options =>
                options.UseInMemoryDatabase(contextName).EnableSensitiveDataLogging());

            return serviceCollection.BuildServiceProvider();
        }
    }
}