namespace Comrade.Api.Modules.Common;

public class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        var result = Regex.Replace(value?.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2")
            .ToLower(CultureInfo.CurrentCulture);

        return value == null ? null : result;
    }
}
