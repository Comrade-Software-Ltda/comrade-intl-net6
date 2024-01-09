namespace Comrade.UnitTests.Tests.TDD.BotMovement.TestDatas;

internal class BotMovementTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new BotMovementInput
            (
                1, 1, 104, 61
            ),
            true
        };
        yield return new object[]
        {
            new BotMovementInput
            (
                1, 1, 50, 20
            ),
            false
        };
        yield return new object[]
        {
            new BotMovementInput
            (
                1, 1, 0, 0
            ),
            false
        };
        yield return new object[]
        {
            new BotMovementInput
            (
                0, 0, 0, 0
            ),
            true
        };
        yield return new object[]
        {
            new BotMovementInput
            (
                0, 0, 1, 1
            ),
            false
        };
        yield return new object[]
        {
            new BotMovementInput
            (
                4, 5, 40, 2
            ),
            false
        };
        yield return new object[]
        {
            new BotMovementInput
            (
                4, 5, 2, 50
            ),
            false
        };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
