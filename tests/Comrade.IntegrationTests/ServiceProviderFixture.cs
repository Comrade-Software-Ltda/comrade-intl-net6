using System;
using Comrade.Application.Caches;
using Comrade.Application.Caches.FunctionCache;
using Comrade.Application.Notifications.Email;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Comrade.IntegrationTests;

public sealed class ServiceProviderFixture : IDisposable
{
    public ServiceProviderFixture()
    {
        var serviceCollection = GetServiceCollection.Execute();

        var dbName = $"test_db_{Guid.NewGuid()}";

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    ["MongoDbContextSettings:ConnectionString"] = "mongodb://localhost/local",
                    ["MongoDbContextSettings:DatabaseName"] = dbName
                }!)
            .Build();


        serviceCollection.AddSingleton<IConfiguration>(configuration);

        var connString =
            configuration.GetValue<string>("MongoDbContextSettings:ConnectionString");

        serviceCollection.AddDbContext<ComradeContext>(options =>
            options.UseInMemoryDatabase(dbName).EnableSensitiveDataLogging()
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

        serviceCollection.Configure<MongoDbContextSettings>(
            configuration.GetSection(nameof(MongoDbContextSettings)));
        serviceCollection.AddSingleton<IMongoDbContextSettings>(x =>
            x.GetRequiredService<IOptions<MongoDbContextSettings>>().Value);

        serviceCollection.Configure<MailKitSettings>(
            configuration.GetSection(nameof(MailKitSettings)));

        serviceCollection.AddSingleton<IMailKitSettings>(sp =>
            sp.GetRequiredService<IOptions<MailKitSettings>>().Value);

        serviceCollection.AddSingleton<IRedisCacheService, RedisCacheService>();
        serviceCollection.AddSingleton<IRedisCacheFunctionService, RedisCacheFunctionService>();
        serviceCollection.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "TesteSistema";
            options.Configuration = "localhost:6379";
        });

        var sp = serviceCollection.BuildServiceProvider();
        Sp = sp;
        Mediator = sp.GetRequiredService<IMediator>();
        RedisCacheFunctionService = sp.GetRequiredService<IRedisCacheFunctionService>();
        SqlContextFixture = sp.GetService<ComradeContext>()!;
        var mongoDbContextSettings = new MongoDbContextSettings
        {
            ConnectionString = connString!,
            DatabaseName = dbName
        };
        MongoDbContextFixtureSettings = mongoDbContextSettings;
        MongoDbContextFixture = new MongoDbContext(mongoDbContextSettings);
    }

    public IServiceProvider Sp { get; }
    public IMediator Mediator { get; }
    public IRedisCacheFunctionService RedisCacheFunctionService { get; }
    public ComradeContext SqlContextFixture { get; }
    public MongoDbContextSettings MongoDbContextFixtureSettings { get; }
    public MongoDbContext MongoDbContextFixture { get; }

    public void Dispose()
    {
        var client = new MongoClient(MongoDbContextFixtureSettings.ConnectionString);
        client.DropDatabase(MongoDbContextFixtureSettings.DatabaseName);
    }
}
