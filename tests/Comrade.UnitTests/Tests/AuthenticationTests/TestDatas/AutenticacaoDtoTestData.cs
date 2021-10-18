using Comrade.Application.Services.AuthenticationServices.Dtos;

namespace Comrade.UnitTests.Tests.AuthenticationTests.TestDatas;

internal class AuthenticationDtoTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            200, new AuthenticationDto
            {
                Key = "1",
                Password = "123456"
            }
        };
        yield return new object[]
        {
            400, new AuthenticationDto
            {
                Key = "",
                Password = "123456"
            }
        };
        yield return new object[]
        {
            1001, new AuthenticationDto
            {
                Key = "3",
                Password = ""
            }
        };
        yield return new object[]
        {
            1001, new AuthenticationDto
            {
                Key = "4",
                Password = "1234567"
            }
        };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}