using Comrade.Core.AirplaneCore.Commands;

namespace Comrade.UnitTests.Tests.AirplaneTests.TestDatas;

internal class AirplaneEditTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            204,
            new AirplaneEditCommand
            {
                Id = new Guid("063f44b8-df8b-4f96-889a-75b9d67c546f"),
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            }
        };
        yield return new object[]
        {
            404,
            new AirplaneEditCommand
            {
                Id = new Guid("00000000-df8b-4f96-889a-75b9d67c546f"),
                Code = "123",
                PassengerQuantity = 456
            }
        };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
