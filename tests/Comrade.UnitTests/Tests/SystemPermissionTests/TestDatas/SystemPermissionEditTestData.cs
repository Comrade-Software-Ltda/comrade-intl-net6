using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Enums;

namespace Comrade.UnitTests.Tests.SystemPermissionTests.TestDatas;

internal class SystemPermissionEditTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            EnumResponse.NoContent,
            new SystemPermissionEditCommand
            {
                Id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"),
                Name = "ACESSO NOVO",
                Tag = "ACEN"
            }
        };
        yield return new object[]
        {
            EnumResponse.NotFound,
            new SystemPermissionEditCommand
            {
                Id = new Guid("00000000-1b83-46f2-91eb-0c64f1c638a1"),
                Name = "ACESSO NOVO",
                Tag = "ACEN"
            }
        };
        yield return new object[]
        {
            EnumResponse.ErrorBusinessValidation,
            new SystemPermissionEditCommand
            {
                Id = new Guid("6adf10d0-1b83-46f2-91eb-0c64f1c638a1"),
                Name = "ACESSO",
                Tag = "  ace  "
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
