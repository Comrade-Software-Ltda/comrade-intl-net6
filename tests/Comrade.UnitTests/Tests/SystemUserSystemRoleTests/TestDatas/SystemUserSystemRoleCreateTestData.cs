using Comrade.Core.SystemUserSystemRoleCore.Commands;
using Comrade.Domain.Enums;

namespace Comrade.UnitTests.Tests.SystemUserSystemRoleTests.TestDatas;

internal class SystemUserSystemRoleCreateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            EnumResponse.Created,
            new SystemUserSystemRoleCreateCommand
            {
                SystemUserId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a5"),
                SystemRoleId = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a7")
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}