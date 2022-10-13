using Comrade.Core.AirplaneCore.Commands;

namespace Comrade.UnitTests.Tests.AirplaneTests.TestDatas;

internal class AirplaneCreateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            201, new AirplaneCreateCommand
            {
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            }
        };
        yield return new object[]
        {
            400, new AirplaneCreateCommand
            {
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
