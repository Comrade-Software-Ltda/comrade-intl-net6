using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToSnakeCaseTests
{
    [Fact]
    public void StringExtension_ToSnakeCase()
    {
        var testObject = "Last in Line";
        var goal = "last_in_line";

        var result = testObject.ToSnakeCase();

        Assert.NotEmpty(result);
        Assert.Equal(goal, result);
    }
}
