namespace Comrade.UnitTests.Tests.TDD.PrimeEvaluation.TestDatas;

internal class PrimeTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            2, new PrimeNumberResult
            (
                false,
                1
            )
        };
        yield return new object[]
        {
            3, new PrimeNumberResult
            (
                true,
                1
            )
        };
        yield return new object[]
        {
            4, new PrimeNumberResult
            (
                false,
                1
            )
        };
        yield return new object[]
        {
            5, new PrimeNumberResult
            (
                true,
                2
            )
        };
        yield return new object[]
        {
            6, new PrimeNumberResult
            (
                false,
                1
            )
        };
        yield return new object[]
        {
            7, new PrimeNumberResult
            (
                true,
                2
            )
        };
        yield return new object[]
        {
            8, new PrimeNumberResult
            (
                false,
                1
            )
        };
        yield return new object[]
        {
            9, new PrimeNumberResult
            (
                false,
                2
            )
        };
        yield return new object[]
        {
            10, new PrimeNumberResult
            (
                false,
                1
            )
        };
        yield return new object[]
        {
            101, new PrimeNumberResult
            (
                true,
                6
            )
        };
        yield return new object[]
        {
            1001, new PrimeNumberResult
            (
                false,
                4
            )
        };
        yield return new object[]
        {
            999331, new PrimeNumberResult
            (
                true,
                500
            )
        };
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
