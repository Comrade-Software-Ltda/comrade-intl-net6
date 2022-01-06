using Comrade.Application.Services.AuthenticationComponent.Dtos;

namespace Comrade.UnitTests.Tests.AuthenticationTests.TestDatas;

internal class AuthenticationDtoTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            200, new AuthenticationDto
            {
                Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
                Password = "123456"
            }
        };
        yield return new object[]
        {
            1001, new AuthenticationDto
            {
                Key = new Guid("E44010d0-1b83-46f2-91eb-0c64f1c638a5"),
                Password = "123456"
            }
        };
        yield return new object[]
        {
            1001, new AuthenticationDto
            {
                Key = new Guid(),
                Password = ""
            }
        };
        yield return new object[]
        {
            1001, new AuthenticationDto
            {
                Key = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
                Password = "1234567"
            }
        };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}