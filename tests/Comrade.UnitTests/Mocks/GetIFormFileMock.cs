using Comrade.Persistence.Extensions;
using Microsoft.AspNetCore.Http;

namespace Comrade.UnitTests.Mocks;

public class GetIFormFileMock
{
    public static async Task<IFormFile> Execute()
    {
        var fileMock = new Mock<IFormFile>();
        var jsonPath = "Comrade.Persistence.SeedData.Sheets";
        var filePath = $"{jsonPath}.basicSheet.xlsx";
        var fileName = "basicSheet.xlsx";
        var assembly = Assembly.GetAssembly(typeof(JsonUtilities));
        if (assembly is not null)
        {
            var file = assembly.GetManifestResourceStream(filePath);
            var ms = new MemoryStream();
            if (file != null)
            {
                await file.CopyToAsync(ms);
                ms.Position = 0;
                fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
                fileMock.Setup(_ => _.FileName).Returns(fileName);
                fileMock.Setup(_ => _.Length).Returns(ms.Length);
            }
        }

        return fileMock.Object;
    }
}
