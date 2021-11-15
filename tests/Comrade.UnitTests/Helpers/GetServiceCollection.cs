using Comrade.Api;
using Comrade.Api.Modules;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Api.Modules.Common.Swagger;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Lookups;
using Comrade.Application.PipelineBehaviors;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Extensions;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;
using FluentValidation;
using MediatR;

namespace Comrade.UnitTests.Helpers;

public static class GetServiceCollection
{
    public static ServiceCollection Execute()
    {
        var services = new ServiceCollection();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        services
            .AddFeatureFlags(configuration)
            .AddEntityRepository()
            .AddAuthentication(configuration)
            .AddVersioning()
            .AddSwagger()
            .AddUseCases()
            .AddCustomControllers()
            .AddCustomCors()
            .AddProxy()
            .AddCustomDataProtection();

        services.AddAutoMapperSetup();
        services.AddLogging();
        services.AddHealthChecks().AddCheck<MemoryHealthCheck>("Memory");

        services.AddScoped<IMongoDbCommandContext, MongoDbContext>();

        services.AddScoped<IMongoDbCommandContext, MongoDbContext>();
        services.AddScoped<IMongoDbQueryContext, MongoDbContext>();
        services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddMediatR(typeof(Startup));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<EntityDto>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<HashingOptions>();

        return services;
    }
}