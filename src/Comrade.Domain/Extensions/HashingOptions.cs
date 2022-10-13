namespace Comrade.Domain.Extensions;

public sealed class HashingOptions
{
    public int Iterations { get; set; } = 100;

    public static implicit operator HashingOptions(int iterations)
    {
        return new HashingOptions();
    }

    public static HashingOptions ToHashingOptions()
    {
        return new HashingOptions();
    }
}
