using Comrade.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using ILogger = Serilog.ILogger;

namespace Comrade.Persistence.ADO;

public class MigrateDatabase
{
    private static readonly ILogger Logger = Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    private readonly GetAllDatabases _getAllDatabases;

    public MigrateDatabase(GetAllDatabases getAllDatabases)
    {
        _getAllDatabases = getAllDatabases;
    }

    public async Task Execute()
    {
        var tenants = GetConfiguredTenants();

        var tasks = tenants.Select(t => MigrateTenantDatabase(t));
        try
        {
            Logger.Information("Starting parallel execution of pending migrations...");
            await Task.WhenAll(tasks);
        }
        catch
        {
            Logger.Warning("Parallel execution of pending migrations is complete with error(s).");
        }

        Logger.Information("Parallel execution of pending migrations is complete.");
    }

    private static async Task MigrateTenantDatabase(MigratorTenantInfo tenant)
    {
        using var logContext = LogContext.PushProperty("TenantName", $"({tenant.Name}) ");
        var dbContextOptions = CreateDefaultDbContextOptions(tenant.ConnectionString);
        try
        {
            using var context = new ComradeContext(dbContextOptions);
            await context.Database.MigrateAsync();
        }
        catch (Exception e)
        {
            Logger.Error(e, "Error occurred during migration");
            throw;
        }
    }

    private static DbContextOptions CreateDefaultDbContextOptions(string connectionString)
    {
        return new DbContextOptionsBuilder()
            .LogTo(Logger.Information, MigrationInfoLogFilter(), DbContextLoggerOptions.None)
            .UseSqlServer(connectionString)
            .Options;
    }

    private List<MigratorTenantInfo> GetConfiguredTenants()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", false);

        IConfiguration config = builder.Build();


        var teste = _getAllDatabases.Execute();

        var oto = new List<MigratorTenantInfo>();
        oto.Add(new MigratorTenantInfo
        {
            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=" + "qweprimeiro" +
                               ";Trusted_Connection=True;MultipleActiveResultSets=true",
            Name = "qweprimeiro"
        });

        return oto;
    }


    private static Func<EventId, LogLevel, bool> MigrationInfoLogFilter()
    {
        return (eventId, level) =>
            level > LogLevel.Information ||
            (level == LogLevel.Information &&
             new[]
             {
                 RelationalEventId.MigrationApplying,
                 RelationalEventId.MigrationAttributeMissingWarning,
                 RelationalEventId.MigrationGeneratingDownScript,
                 RelationalEventId.MigrationGeneratingUpScript,
                 RelationalEventId.MigrationReverting,
                 RelationalEventId.MigrationsNotApplied,
                 RelationalEventId.MigrationsNotFound,
                 RelationalEventId.MigrateUsingConnection
             }.Contains(eventId));
    }
}
