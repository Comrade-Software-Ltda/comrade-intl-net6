using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToCamelCaseTests
{
    [Fact]
    public void StringExtension_ToCamelCase()
    {
        var testObject = "Last in Line";
        var goal = "lastInLine";

        var result = testObject.ToCamelCase();

        Assert.NotEmpty(result);
        Assert.Equal(goal, result);
    }
}