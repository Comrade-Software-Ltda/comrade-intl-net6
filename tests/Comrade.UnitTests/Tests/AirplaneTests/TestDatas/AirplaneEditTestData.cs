using Comrade.Domain.Models;

namespace Comrade.UnitTests.Tests.AirplaneTests.TestDatas;

internal class AirplaneEditTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            200, new Airplane
            {
                Id = 1,
                Code = "123",
                Model = "234",
                PassengerQuantity = 456
            }
        };
        yield return new object[]
        {
            400, new Airplane
            {
                Id = 2,
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