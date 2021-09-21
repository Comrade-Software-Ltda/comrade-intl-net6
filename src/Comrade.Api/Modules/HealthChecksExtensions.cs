#region

using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Domain.Extensions;
using Comrade.Persistence.DataAccess;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.FeatureManagement;
using System.Net.Mime;
using System.Text.Json;

#endregion

namespace Comrade.Api.Modules;

/// <summary>
///     HealthChecks Extensions.
/// </summary>
public static class HealthChecksExtensions
{
    /// <summary>
    ///     Add Health Checks dependencies varying on configuration.
    /// </summary>
    public static IServiceCollection AddHealthChecks(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        IHealthChecksBuilder healthChecks = services.AddHealthChecks();

        IFeatureManager featureManager = services
            .BuildServiceProvider()
            .GetRequiredService<IFeatureManager>();

        var healthChecksIsEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.HealthChecks))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var sqlServerIsEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.MsSqlServer))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var isPostgresSqlEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.PostgresSql))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        if (healthChecksIsEnabled)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<ComradeContext>("ComradeContext")
                .AddApplicationInsightsPublisher();

            services.AddHealthChecksUI().AddInMemoryStorage();

            if (sqlServerIsEnabled)
            {
                healthChecks.AddSqlServer(
                    configuration.GetValue<string>("PersistenceModule:MsSqlDb"),
                    name: "ms-sql", tags: new[] { "db", "data" });
            }

            if (isPostgresSqlEnabled)
            {
                healthChecks.AddNpgSql(
                    configuration.GetValue<string>("PersistenceModule:PostgresSqlDb"),
                    name: "postgres-sql", tags: new[] { "db", "data" });
            }
        }


        return services;
    }

    /// <summary>
    ///     Use Health Checks dependencies.
    /// </summary>
    public static IApplicationBuilder UseHealthChecks(
        this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = WriteResponse
        });

        app.UseHealthChecks("/healthcheck", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        }).UseHealthChecksUI(options =>
        {
            options.UIPath = "/monitor";
            options.ApiPath = "/monitor-api";
        });

        return app;
    }

    private static Task WriteResponse(HttpContext context, HealthReport result)
    {
        var testObject = JsonSerializer.Serialize(
            new
            {
                currentTime = DateTimeBrasilia.GetDateTimeBrasilia(),
                statusApplication = result.Status.ToString(),
                healthChecks = result.Entries.Select(e => new
                {
                    check = e.Key,
                    status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                })
            });

        context.Response.ContentType = MediaTypeNames.Application.Json;
        return context.Response.WriteAsync(testObject);
    }
}
