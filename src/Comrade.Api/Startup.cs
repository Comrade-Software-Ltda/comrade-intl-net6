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
using Comrade.Persistence.ADO;
using Comrade.Persistence.Bases;
using Comrade.Persistence.DataAccess;
using FluentValidation;
using MediatR;

namespace Comrade.Api;

/// <summary>
///     Startup.
/// </summary>
/// <remarks>
///     Startup constructor.
/// </remarks>
public sealed class Startup(IConfiguration configuration)
{
    private IConfiguration Configuration { get; } = configuration;

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
            options.InstanceName = "COM1";
            options.Configuration = "localhost:6379";
        });
        services.AddAutoMapperSetup();
        services.AddLogging();

        services.Configure<MongoDbContextSettings>(
            Configuration.GetSection(nameof(MongoDbContextSettings)));

        services.AddSingleton<IMongoDbContextSettings>(sp =>
            sp.GetRequiredService<IOptions<MongoDbContextSettings>>().Value);

        services.Configure<MailKitSettings>(
            Configuration.GetSection(nameof(MailKitSettings)));

        services.AddSingleton<IMailKitSettings>(sp =>
            sp.GetRequiredService<IOptions<MailKitSettings>>().Value);

        services.AddScoped<GetAllDatabases>();
        services.AddScoped<MigrateDatabase>();
        services.AddScoped<CreateDatabase>();

        services.AddScoped<IMongoDbCommandContext, MongoDbContext>();
        services.AddScoped<IMongoDbQueryContext, MongoDbContext>();
        services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddMediatR(config => { config.RegisterServicesFromAssembly(typeof(Startup).Assembly); });

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
            .UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
