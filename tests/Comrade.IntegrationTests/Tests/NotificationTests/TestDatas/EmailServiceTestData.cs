using System.Collections;

namespace Comrade.IntegrationTests.Tests.NotificationTests.TestDatas;

internal class EmailServiceTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "abraaoribeiroo23@gmail.com",
            "Teste de Email",
            "Corpo do Email"
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
