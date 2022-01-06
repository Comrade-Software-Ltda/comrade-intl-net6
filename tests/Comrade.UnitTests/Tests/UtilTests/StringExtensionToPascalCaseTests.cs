using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToPascalCaseTests
{
    [Fact]
    public void StringExtension_ToPascalCase()
    {
        var testObject = "Last in Line";
        var goal = "LastInLine";

        var result = testObject.ToPascalCase();

        Assert.NotEmpty(result);
        Assert.Equal(goal, result);
    }
}