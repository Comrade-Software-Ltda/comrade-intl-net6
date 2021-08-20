#region

using Comrade.Api.Modules.Common.FeatureFlags;
using Comrade.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

#endregion

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
        IFeatureManager featureManager = services
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


        if (isMsSqlServerEnabled)
        {
            services.AddDbContext<ComradeContext>(options =>
                options.UseSqlServer(
                    configuration.GetValue<string>("PersistenceModule:MsSqlDb")));
        }
        else if (isPostgresSqlEnabled)
        {
            services.AddDbContext<ComradeContext>(options =>
                options.UseNpgsql(
                    configuration.GetValue<string>("PersistenceModule:PostgresSqlDb")));
        }
        else
        {
            services.AddDbContext<ComradeContext>(options =>
                options.UseInMemoryDatabase("test_database").EnableSensitiveDataLogging());
            ComradeContextFake.AddDataFakeContext(services);
        }

        return services;
    }
}
