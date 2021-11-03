using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Comrade.Api.Modules;

/// <summary>
///     Persistence Extensions.
/// </summary>
public static class PersistenceExtensions
{
    /// <summary>
    ///     Add Persistence dependencies varying on configuration.
    /// </summary>
    public static IServiceCollection AddSqlServer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var featureManager = services
            .BuildServiceProvider()
            .GetRequiredService<IFeatureManager>();

        var isMsSqlServerEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.MsSqlServer))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var isPostgresSqlEnabled = featureManager
            .IsEnabledAsync(nameof(CustomFeature.PostgresSql))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        var injectInitialData = featureManager
            .IsEnabledAsync(nameof(CustomFeature.InjectInitialData))
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        if (isMsSqlServerEnabled)
        {
            services.AddDbContext<ComradeContext>(options =>
                options.UseSqlServer(
                    configuration.GetValue<string>("PersistenceModule:MsSqlDbConnection")));
        }
        else if (isPostgresSqlEnabled)
        {
            services.AddDbContext<ComradeContext>(options =>
                options.UseNpgsql(
                    configuration.GetValue<string>("PersistenceModule:PostgresSqlDbConnection")));
        }
        else
        {
            services.AddDbContext<ComradeContext>(options =>
                options.UseInMemoryDatabase("test_database").EnableSensitiveDataLogging()
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

            if (injectInitialData)
            {
                var context = services.BuildServiceProvider()
                    .GetService<ComradeContext>();
                ComradeMemoryContextFake.AddDataFakeContext(context);
            }
        }

        return services;
    }
}