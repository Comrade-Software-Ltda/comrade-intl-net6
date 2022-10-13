using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToInt64Tests
{
    [Fact]
    public void StringExtension_ToInt64()
    {
        var testObject = "556";
        long goal = 556;

        var result = testObject.ToInt64();

        Assert.Equal(goal, result);
    }
}
