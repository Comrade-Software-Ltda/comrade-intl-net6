#region

using Comrade.Api.Modules.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Serilog;
using Serilog.Extensions.Logging;

#endregion

namespace Comrade.Api;


/// <summary>
/// </summary>
public static class Program
{
    private static readonly LoggerProviderCollection Providers = new();

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        var hostBuilder = CreateHostBuilder(args).Build();
        try
        {
            Log.Information("Starting up");
            hostBuilder.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }


    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.AddCommandLine(args);
                LoggingExtensions.CreateLogMongoDb(Providers);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseSerilog(providers: Providers);
    }
}
