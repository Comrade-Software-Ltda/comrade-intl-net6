using Comrade.Api;
using Comrade.Api.Modules;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Api.Modules.Common.Swagger;
using Comrade.Application.Bases;
using Comrade.Application.Lookups;
using Comrade.Application.PipelineBehaviors;
using Comrade.Application.Services.AirplaneServices.Handlers;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Extensions;
using Comrade.Persistence.Bases;
using FluentValidation;
using MediatR;

namespace Comrade.UnitTests.Helpers;

public static class GetServiceProviderMemDb
{
    public static ServiceProvider Execute()
    {
        var services = new ServiceCollection();

        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        services
            .AddFeatureFlags(configuration)
            .AddSqlServer(configuration)
            .AddEntityRepository()
            .AddHealthChecks(configuration)
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


        services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddMediatR(typeof(Startup));
        services.AddMediatR(typeof(AirplaneCreateHandler).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<EntityDto>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<HashingOptions>();

        return services.BuildServiceProvider();
    }
}