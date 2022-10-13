namespace Comrade.Api.Modules.Common;

/// <summary>
///     CORS Extensions.
/// </summary>
public static class CustomCorsExtensions
{
    private const string AllowsAny = "_allowsAny";

    /// <summary>
    ///     Add CORS.
    /// </summary>
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowsAny,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });


        return services;
    }

    /// <summary>
    ///     Use CORS.
    /// </summary>
    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors(AllowsAny);
        return app;
    }
}
