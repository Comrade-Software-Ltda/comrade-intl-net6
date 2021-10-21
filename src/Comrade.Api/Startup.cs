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

namespace Comrade.Api;

/// <summary>
///     Startup.
/// </summary>
public sealed class Startup
{
    /// <summary>
    ///     Startup constructor.
    /// </summary>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    /// <summary>
    ///     Configure dependencies from application.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddFeatureFlags(Configuration)
            .AddSqlServer(Configuration)
            .AddEntityRepository()
            .AddHealthChecks(Configuration)
            .AddAuthentication(Configuration)
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
        services.AddMediatR(typeof(CreateAirplaneHandler).GetTypeInfo().Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<EntityDto>();
        ;

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<HashingOptions>();
    }

    /// <summary>
    ///     Configure http request pipeline.
    /// </summary>
    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            app.UseExceptionHandler("/api/V1/CustomError")
                .UseHsts();

        app
            .UseProxy(Configuration)
            .UseHealthChecks()
            .UseCustomCors()
            .UseCustomHttpMetrics()
            .UseRouting()
            .UseVersionedSwagger(provider, Configuration)
            .UseAuthentication()
            .UseAuthorization()
            .UseSerilogRequestLogging()
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}