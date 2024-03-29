﻿using Comrade.Application.Extensions;

namespace Comrade.UnitTests.Tests.UtilTests;

public class StringExtensionToDecimalTests
{
    [Fact]
    public void StringExtension_ToDecimal()
    {
        var testObject = "420.55";
        var goal = new decimal(420.55);

        var result = testObject.ToDecimal();

        Assert.Equal(goal, result);
    }
}
