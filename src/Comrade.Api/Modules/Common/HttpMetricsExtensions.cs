namespace Comrade.Api.Modules.Common;

/// <summary>
///     Http Metrics Extensions.
/// </summary>
public static class HttpMetricsExtensions
{
    /// <summary>
    ///     Add Prometheus dependencies.
    /// </summary>
    public static IApplicationBuilder UseCustomHttpMetrics(this IApplicationBuilder appBuilder)
    {
        return appBuilder;
    }
}
