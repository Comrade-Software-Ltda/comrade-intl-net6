using System.Threading;

namespace Comrade.Api.Modules;

public class MemoryHealthCheck : IHealthCheck
{
    private readonly IOptionsMonitor<MemoryCheckOptions> _options;

    public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
    {
        _options = options;
    }

    public string Name => "memory_check";

    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var options = _options.Get(context.Registration.Name);

        var allocated = GC.GetTotalMemory(false);
        var data = new Dictionary<string, object>
        {
            {"AllocatedBytes", allocated},
            {"Gen0Collections", GC.CollectionCount(0)},
            {"Gen1Collections", GC.CollectionCount(1)},
            {"Gen2Collections", GC.CollectionCount(2)}
        };
        var status = allocated < options.Threshold
            ? HealthStatus.Healthy
            : context.Registration.FailureStatus;

        return Task.FromResult(new HealthCheckResult(
            status,
            "Reports degraded status if allocated bytes " +
            $">= {options.Threshold} bytes.",
            null,
            data));
    }
}

public class MemoryCheckOptions
{
    public long Threshold { get; set; } = 1024L * 1024L * 1024L;
}
