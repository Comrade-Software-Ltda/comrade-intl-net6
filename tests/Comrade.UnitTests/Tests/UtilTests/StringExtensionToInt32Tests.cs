using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToInt32Tests
{
    [Fact]
    public void StringExtension_ToInt32()
    {
        var testObject = "55";
        var goal = 55;

        var result = testObject.ToInt32();

        Assert.Equal(goal, result);
    }
}