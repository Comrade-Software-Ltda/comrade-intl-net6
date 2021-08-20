#region

using Comrade.Application.Spreadsheets.SpreadsheetFunctions;
using Comrade.UnitTests.Mocks;
using Xunit;

#endregion

namespace Comrade.UnitTests.Tests.ImportTests;

public class ReadExcelFileSaxTests
{
    private readonly GetIFormFileMock _getIFormFileMock = new();

    [Fact]
    public async Task ReadExcelFileSaxTest()
    {
        var file = await _getIFormFileMock.Execute();

        var result = ReadExcelFileSax.Execute(file);

        Assert.NotEmpty(result);
        Assert.Equal(10, result.Count);
    }
}
