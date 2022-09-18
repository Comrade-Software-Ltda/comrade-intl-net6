using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Comrade.Persistence.DataAccess;

/// <summary>
///     ContextFactory.
/// </summary>
public sealed class ContextFactory : IDesignTimeDbContextFactory<ComradeContext>
{
    /// <summary>
    ///     Instantiate a ComradeContext.
    /// </summary>
    /// <param name="args">Command line args.</param>
    /// <returns>Comrade Context.</returns>
    public ComradeContext CreateDbContext(string[] args)
    {
        string connectionString = ReadDefaultConnectionStringFromAppSettings();

        DbContextOptionsBuilder<ComradeContext> builder = new DbContextOptionsBuilder<ComradeContext>();
        Console.WriteLine(connectionString);
        builder.UseSqlServer(connectionString);
        builder.EnableSensitiveDataLogging();
        return new ComradeContext(builder.Options);
    }

    private static string ReadDefaultConnectionStringFromAppSettings()
    {
        string? envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{envName}.json", false)
            .AddEnvironmentVariables()
            .Build();

        string connectionString = configuration.GetValue<string>("PersistenceModule:MsSqlDbConnection");
        return connectionString;
    }
}
