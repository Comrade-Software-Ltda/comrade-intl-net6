using Comrade.Core.AirplaneCore.Commands;
using Comrade.Persistence.DataAccess;
using Comrade.UnitTests.DataInjectors;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Comrade.UnitTests.Tests.AirplaneTests.TestDatas;
using Xunit;

namespace Comrade.UnitTests.Tests.AirplaneTests;

public sealed class UcAirplaneEditTests
{
    [Theory]
    [ClassData(typeof(AirplaneEditTestData))]
    public async Task UcAirplaneEdit_Test(int expected, AirplaneEditCommand testObjectInput)
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseInMemoryDatabase("test_database_UcAirplaneEdit_Test" + testObjectInput.Id)
            .EnableSensitiveDataLogging().Options;

        await using var context = new ComradeContext(options);
        await context.Database.EnsureCreatedAsync();
        InjectDataOnContextBase.InitializeDbForTests(context);

        var ucAirplaneEdit = UcAirplaneInjection.GetUcAirplaneEdit(context);
        var result = await ucAirplaneEdit.Execute(testObjectInput);

        Assert.Equal(expected, result.Code);
    }

    [Fact]
    public async Task UcAirplaneEdit_Test_Error()
    {
        var options = new DbContextOptionsBuilder<ComradeContext>()
            .UseSqlServer("error")
            .EnableSensitiveDataLogging().Options;

        var testObject = new AirplaneEditCommand
        {
            Id = 1,
            Code = "123",
            Model = "234",
            PassengerQuantity = 456
        };

        await using var context = new ComradeContext(options);

        var ucAirplaneEdit = UcAirplaneInjection.GetUcAirplaneEdit(context);
        try
        {
            var result = await ucAirplaneEdit.Execute(testObject);
            Assert.True(false);
        }
        catch (Exception e)
        {
            Assert.NotEmpty(e.Message);
        }
    }
}