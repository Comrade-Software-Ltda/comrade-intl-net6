using Comrade.Application.Extensions;
using Xunit;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToSlugCaseTests
{
    [Fact]
    public void StringExtension_ToSlugCase()
    {
        var testObject = "Last in Line";
        var goal = "last-in-line";

        var result = testObject.ToSlug();

        Assert.NotEmpty(result);
        Assert.Equal(goal, result);
    }
}
