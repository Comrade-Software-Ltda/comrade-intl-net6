namespace Comrade.Domain.Extensions;

public sealed class HashingOptions
{
    public int Iterations { get; set; } = 100;

    public static implicit operator HashingOptions(int v)
    {
        return new();
    }

    public static HashingOptions ToHashingOptions()
    {
        return new();
    }
}
