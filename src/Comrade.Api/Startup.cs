using Comrade.Api.Modules;
using Comrade.Api.Modules.Common;
using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Api.Modules.Common.Swagger;
using Comrade.Application.Bases;
using Comrade.Application.Bases.Interfaces;
using Comrade.Application.Caches;
using Comrade.Application.Lookups;
using Comrade.Application.Notifications.Email;
using Comrade.Application.PipelineBehaviors;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Extensions;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;
using FluentValidation;
using HealthChecks.UI.Client;
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
            .AddAuthentication(Configuration)
            .AddVersioning()
            .AddSwagger()
            .AddUseCases()
            .AddCustomControllers()
            .AddCustomCors()
            .AddProxy()
            .AddCustomDataProtection();

        services.AddSingleton<IRedisCacheService, RedisCacheService>();
        services.AddStackExchangeRedisCache(options =>
        {
            options.InstanceName = "Sistema";
            options.Configuration = "localhost:6379";
        });
        services.AddAutoMapperSetup();
        services.AddLogging();
        services.AddHealthChecks().AddCheck<MemoryHealthCheck>("Memory");

        services.Configure<MongoDbContextSettings>(
            Configuration.GetSection(nameof(MongoDbContextSettings)));

        services.AddSingleton<IMongoDbContextSettings>(sp =>
            sp.GetRequiredService<IOptions<MongoDbContextSettings>>().Value);

        services.Configure<MailKitSettings>(
            Configuration.GetSection(nameof(MailKitSettings)));

        services.AddSingleton<IMailKitSettings>(sp =>
            sp.GetRequiredService<IOptions<MailKitSettings>>().Value);

        services.AddScoped<IMongoDbCommandContext, MongoDbContext>();
        services.AddScoped<IMongoDbQueryContext, MongoDbContext>();
        services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddMediatR(typeof(Startup));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssemblyContaining<EntityDto>();
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
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/api/V1/CustomError")
                .UseHsts();
        }

        app
            .UseProxy(Configuration)
            .UseCustomCors()
            .UseCustomHttpMetrics()
            .UseRouting()
            .UseVersionedSwagger(provider, Configuration)
            .UseAuthentication()
            .UseAuthorization()
            .UseSerilogRequestLogging()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapControllers();
            });
    }
}