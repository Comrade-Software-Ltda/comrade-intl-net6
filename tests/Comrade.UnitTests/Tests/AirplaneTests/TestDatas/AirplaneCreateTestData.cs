using Comrade.Domain.Models;

namespace Comrade.UnitTests.Tests.AirplaneTests.TestDatas;

internal class AirplaneCreateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            201, new Airplane
            {
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            }
        };
        yield return new object[]
        {
            400, new Airplane
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