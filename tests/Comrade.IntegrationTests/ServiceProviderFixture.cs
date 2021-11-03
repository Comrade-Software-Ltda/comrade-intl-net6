using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;

namespace Comrade.IntegrationTests
{
    public class ServiceProviderFixture : IDisposable
    {
        public ServiceProviderFixture()
        {
            var serviceCollection = GetServiceCollection.Execute();

            var dbName = $"test_db_{Guid.NewGuid()}";

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            var connString = config.GetValue<string>("MongoDbContextSettings:ConnectionString");

            serviceCollection.AddDbContext<ComradeContext>(options =>
                options.UseInMemoryDatabase(dbName).EnableSensitiveDataLogging());

            var sp = serviceCollection.BuildServiceProvider();
            Sp = sp;
            Mediator = sp.GetRequiredService<IMediator>();
            PostgresContextFixture = sp.GetService<ComradeContext>()!;
            var test = new MongoDbContextSettings()
            {
                ConnectionString = connString,
                DatabaseName = dbName,
                BooksCollectionName = dbName
            };
            MongoDbContextFixture = new MongoDbContext(test);
        }

        public IServiceProvider InitiateContext()
        {
            var serviceCollection = GetServiceCollection.Execute();

            var dbName = $"test_db_{Guid.NewGuid()}";

            serviceCollection.AddDbContext<ComradeContext>(options =>
                options.UseInMemoryDatabase(dbName).EnableSensitiveDataLogging());

            return serviceCollection.BuildServiceProvider();
        }

        public IServiceProvider Sp { get; }
        public IMediator Mediator { get; }
        public ComradeContext PostgresContextFixture { get; }
        public MongoDbContext MongoDbContextFixture { get; }

        public void Dispose()
        {
        }
    }
}