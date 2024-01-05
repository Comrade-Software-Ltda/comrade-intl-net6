using Microsoft.AspNetCore.Http;

namespace Comrade.UnitTests.Tests.DeliveryDrone;

public static class GetIFormFileDroneMock
{
    public static async Task<IFormFile> Execute()
    {
        var fileMock = new Mock<IFormFile>();
        var jsonPath = "Comrade.UnitTests.Tests.DeliveryDrone.Data";
        var filePath = $"{jsonPath}.commands.txt";
        var fileName = "commands.txt";
        var assembly = Assembly.GetExecutingAssembly();

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

    public static async Task<IFormFile> ExecuteDifferentFile()
    {
        var fileMock = new Mock<IFormFile>();
        var jsonPath = "Comrade.UnitTests.Tests.DeliveryDrone.Data";
        var filePath = $"{jsonPath}.commands2.txt";
        var fileName = "commands2.txt";
        var assembly = Assembly.GetExecutingAssembly();

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

    public static async Task<IFormFile> ExecuteWrongFile()
    {
        var fileMock = new Mock<IFormFile>();
        var jsonPath = "Comrade.UnitTests.Tests.DeliveryDrone.Data";
        var filePath = $"{jsonPath}.commands3.txt";
        var fileName = "commands3.txt";
        var assembly = Assembly.GetExecutingAssembly();

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

    public static async Task<IFormFile> ExecuteFileWithError()
    {
        var fileMock = new Mock<IFormFile>();
        var jsonPath = "Comrade.UnitTests.Tests.DeliveryDrone.Data";
        var filePath = $"{jsonPath}.commands4.txt";
        var fileName = "commands3.txt";
        var assembly = Assembly.GetExecutingAssembly();

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

    public static IFormFile ExecuteEmptyFile()
    {
        var fileMock = new Mock<IFormFile>();
        var ms = new MemoryStream();
        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ => _.FileName).Returns("emptyFile.txt");
        fileMock.Setup(_ => _.Length).Returns(0);
        return fileMock.Object;
    }
}
