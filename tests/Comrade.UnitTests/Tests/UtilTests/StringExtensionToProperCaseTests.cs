using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToProperCaseTests
{
    [Fact]
    public void StringExtension_ToProperCase()
    {
        var testObject = "Last in Line";
        var goal = "Last In Line";

        var result = testObject.ToProperCase();

        Assert.NotEmpty(result);
        Assert.Equal(goal, result);
    }
}