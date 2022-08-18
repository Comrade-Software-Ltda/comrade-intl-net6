using Comrade.Core.SystemRoleCore.Commands;

namespace Comrade.UnitTests.Tests.SystemRoleTests.TestDatas;

internal class SystemRoleEditTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            204,
            new SystemRoleEditCommand
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "ROLE"
            }
        };
        yield return new object[]
        {
            404,
            new SystemRoleEditCommand
            {
                Id = new Guid("00000000-1b83-46f2-91eb-0c64f1c638a7")
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}