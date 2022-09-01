using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Enums;

namespace Comrade.UnitTests.Tests.SystemUserSystemRoleTests.TestDatas;

internal class SystemUserSystemRoleEditTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            EnumResponse.NoContent,
            new SystemUserSystemRoleEditCommand
            {
                Id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a4"),
                SystemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
                SystemRoleId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            }
        };
        yield return new object[]
        {
            EnumResponse.NotFound,
            new SystemUserSystemRoleEditCommand
            {
                Id = new Guid("00000000-1b83-46f2-91eb-0c64f1c638a4"),
                SystemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
                SystemRoleId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}