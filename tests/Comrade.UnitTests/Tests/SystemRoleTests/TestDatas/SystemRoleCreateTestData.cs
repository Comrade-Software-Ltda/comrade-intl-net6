using Comrade.Core.SystemRoleCore.Commands;
using Comrade.Domain.Enums;

namespace Comrade.UnitTests.Tests.SystemRoleTests.TestDatas;

internal class SystemRoleCreateTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            EnumResponse.Created,
            new SystemRoleCreateCommand
            {
                Name = "ROLE"
            }
        };
        yield return new object[]
        {
            EnumResponse.ErrorBusinessValidation,
            new SystemRoleCreateCommand
            {
                Name = " aDm "
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
