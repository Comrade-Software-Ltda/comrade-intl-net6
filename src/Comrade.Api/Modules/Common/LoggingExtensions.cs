using Serilog.Extensions.Logging;
using Serilog.Sinks.MSSqlServer;

namespace Comrade.Api.Modules.Common;

/// <summary>
/// </summary>
public static class LoggingExtensions
{
    public static void CreateLogMongoDb(LoggerProviderCollection providers)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.With(new ApplicationDetailsEnricher())
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MongoDB("mongodb://localhost/local")
            .WriteTo.Providers(providers)
            .CreateLogger();
    }

    public static void CreateLogSqlServer(LoggerProviderCollection providers,
        IConfigurationRoot configurationRoot)
    {
        var connection = configurationRoot.GetValue<string>("ConnectionStrings:MsSqlDbConnection");

        var columnOptions = new ColumnOptions
        {
            AdditionalColumns = new Collection<SqlColumn>
            {
                new("UserName", SqlDbType.VarChar)
            }
        };

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.With(new ApplicationDetailsEnricher())
            .Enrich.FromLogContext()
            .WriteTo.MSSqlServer(connection,
                new MSSqlServerSinkOptions
                {
                    AutoCreateSqlTable = true,
                    TableName = "log_auth_user_service"
                }, columnOptions: columnOptions)
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MongoDB("mongodb://localhost/local")
            .WriteTo.Providers(providers)
            .CreateLogger();
    }
}