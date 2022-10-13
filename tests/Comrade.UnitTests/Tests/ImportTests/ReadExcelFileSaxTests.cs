using Comrade.Application.Spreadsheets.SpreadsheetFunctions;
using Comrade.UnitTests.Mocks;
using Xunit;

namespace Comrade.UnitTests.Tests.ImportTests;

public class ReadExcelFileSaxTests
{
    [Fact]
    public async Task ReadExcelFileSaxTest()
    {
        var file = await GetIFormFileMock.Execute();

        var result = ReadExcelFileSax.Execute(file);

        Assert.NotEmpty(result);
        Assert.Equal(10, result.Count);
    }
}
