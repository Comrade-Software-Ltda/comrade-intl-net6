using System.IO;
using System.Text;

namespace Comrade.Application.Components.DeliveryOptimizer.Core;

public static class GenerateErrorOutput
{
    public static MemoryStream Execute()
    {
        var result = new StringBuilder();

        result.AppendLine("Invalid input. Please check the data and try again.");

        var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(result.ToString()));
        return memoryStream;
    }
}
