using Comrade.Core.SystemPermissionCore.Commands;
using Comrade.Domain.Enums;

namespace Comrade.UnitTests.Tests.SystemPermissionTests.TestDatas;

internal class SystemPermissionCreateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            EnumResponse.Created,
            new SystemPermissionCreateCommand
            {
                Name = "ACESSO NOVO",
                Tag = "ACEN"
            }
        };
        yield return new object[]
        {
            EnumResponse.ErrorBusinessValidation,
            new SystemPermissionCreateCommand
            {
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
