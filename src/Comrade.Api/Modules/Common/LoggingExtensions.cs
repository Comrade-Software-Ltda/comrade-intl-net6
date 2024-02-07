using Serilog.Extensions.Logging;

namespace Comrade.Api.Modules.Common;

/// <summary>
/// </summary>
public static class LoggingExtensions
{
    public static void CreateLog(LoggerProviderCollection providers)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.With(new ApplicationDetailsEnricher())
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.MongoDB("mongodb://localhost/local")
            .WriteTo.Providers(providers)
            .CreateLogger();
    }
}
