using Comrade.Api.Modules.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Serilog.Extensions.Logging;
using System.Net;

namespace Comrade.Api;

/// <summary>
/// </summary>
public static class Program
{
    private static readonly LoggerProviderCollection Providers = new();

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    [Obsolete]
    public static void Main(string[] args)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

        try
        {
            var hostBuilder = CreateHostBuilder(args).Build();
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
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        if (app.Environment.IsProduction())
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddCommandLine(args);
                    LoggingExtensions.CreateLog(Providers);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .UseKestrel(options =>
                        {
                            // Bind to the port Heroku provides
                            options.Listen(IPAddress.Any,
                                int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "8080"));
                        });
                })
                .UseSerilog((ctx, lc) => lc
                    .WriteTo.Console()
                    .ReadFrom.Configuration(ctx.Configuration));
        }

        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                configApp.AddCommandLine(args);
                LoggingExtensions.CreateLog(Providers);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseSerilog(providers: Providers);
    }
}
