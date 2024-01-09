using Comrade.Application.Extensions;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToKebabCaseTests
{
    [Fact]
    public void StringExtension_ToKebabCase()
    {
        var testObject = "Last in Line";
        var goal = "last-in-line";

        var result = testObject.ToKebabCase();

        Assert.NotEmpty(result);
        Assert.Equal(goal, result);
    }
}
