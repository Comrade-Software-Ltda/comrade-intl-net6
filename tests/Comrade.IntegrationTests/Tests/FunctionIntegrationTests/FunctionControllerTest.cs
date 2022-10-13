using Comrade.Application.Bases;
using Comrade.Application.Caches.FunctionCache;
using Comrade.Domain.Enums;
using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Comrade.IntegrationTests.Tests.FunctionIntegrationTests;

public class FunctionControllerTest : IClassFixture<ServiceProviderFixture>
{
    private readonly IRedisCacheFunctionService _redisCacheFunctionService;

    public FunctionControllerTest(ServiceProviderFixture fixture)
    {
        _redisCacheFunctionService = fixture.RedisCacheFunctionService;
    }

    [Fact]
    public void FunctionController_Alticci_NegativeNumbers()
    {
        var functionController = FunctionInjectionController.GetFunctionController(_redisCacheFunctionService);
        var result = functionController.Alticci(-10);
        // ReSharper disable once InvertIf
        if (result is ObjectResult okResult)
        {
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Null(okResult.Value);
        }
    }

    [Fact]
    public void FunctionController_Alticci()
    {
        var functionController = FunctionInjectionController.GetFunctionController(_redisCacheFunctionService);
        const long n = 10;
        _redisCacheFunctionService.RemoveAllCacheBelowOrEqualFunction(EnumFunction.Alticci, n);
        var result = functionController.Alticci(n);

        if (result is ObjectResult okResult)
        {
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            var actualResultValue = okResult.Value as FunctionReturnDto;
            Assert.NotNull(actualResultValue);
            Assert.Equal(9, actualResultValue?.ResultDto.Fn);
            /* F(n = 10) = 9
             * 10 -> 7 , 8
             *      7 -> 4 , 5
             *          4 -> 1*, 2*
             *              DO_CACHE(1) -> [1]
             *              DO_CACHE(2) -> [1]
             *          DO_CACHE(4) -> CACHE(1) + CACHE(2) = [1] + [1] = [2]
             *          5 -> 2*, 3
             *              USE_CACHE(2) -> [1]
             *              3 -> 0*, 1*
             *                  DO_CACHE(0) -> [0]
             *                  USE_CACHE(1) -> [1]
             *              DO_CACHE(3) -> CACHE(0) + CACHE(1) = [0] + [1] = [1]
             *          DO_CACHE(5) -> CACHE(2) + CACHE(3) = [1] + [1] = [2]
             *      DO_CACHE(7) -> CACHE(4) + CACHE(5) = [2] + [2] = [4]
             *      8 -> 5 , 6
             *          USE_CACHE(5) -> [2]
             *          6 -> 3 , 4
             *              USE_CACHE(3) -> [1]
             *              USE_CACHE(4) -> [2]
             *          DO_CACHE(6) -> CACHE(3) + CACHE(4) = [1] + [2] = [3]
             *      DO_CACHE(8) -> CACHE(5) + CACHE(6) = [2] + [3] = [5]
             * DO_CACHE(10) -> CACHE(7) + CACHE(8) = [4] + [5] = [9]
             *
             * DO_CACHE =  {(1,1),(2,1),(4,2),(0,0),(3,1),(5,2),(7,4),(6,3),(8,5),(10,9)}
             * USE_CACHE = {(2,1),(1,1),(5,2),(3,1),(4,2)}
             */
            var doCache = actualResultValue?.DoCache;
            Assert.NotNull(doCache);
            Assert.Equal(10, doCache.Count);
            Assert.True(doCache[0].N == 1 && doCache[0].Fn == 1);
            Assert.True(doCache[1].N == 2 && doCache[1].Fn == 1);
            Assert.True(doCache[2].N == 4 && doCache[2].Fn == 2);
            Assert.True(doCache[3].N == 0 && doCache[3].Fn == 0);
            Assert.True(doCache[4].N == 3 && doCache[4].Fn == 1);
            Assert.True(doCache[5].N == 5 && doCache[5].Fn == 2);
            Assert.True(doCache[6].N == 7 && doCache[6].Fn == 4);
            Assert.True(doCache[7].N == 6 && doCache[7].Fn == 3);
            Assert.True(doCache[8].N == 8 && doCache[8].Fn == 5);
            Assert.True(doCache[9].N == 10 && doCache[9].Fn == 9);

            var useCache = actualResultValue?.UseCache;
            Assert.NotNull(useCache);
            Assert.Equal(5, useCache.Count);
            Assert.True(useCache[0].N == 2 && useCache[0].Fn == 1);
            Assert.True(useCache[1].N == 1 && useCache[1].Fn == 1);
            Assert.True(useCache[2].N == 5 && useCache[2].Fn == 2);
            Assert.True(useCache[3].N == 3 && useCache[3].Fn == 1);
            Assert.True(useCache[4].N == 4 && useCache[4].Fn == 2);
        }

        result = functionController.Alticci(n - 1);

        if (result is ObjectResult okResult2)
        {
            Assert.Equal(StatusCodes.Status200OK, okResult2.StatusCode);
            var actualResultValue = okResult2.Value as FunctionReturnDto;
            Assert.NotNull(actualResultValue);
            Assert.Equal(7, actualResultValue.ResultDto.Fn);
            /* F(n = 9) = 7
             * 9 -> 6 , 7
             *      USE_CACHE(6) -> [3]
             *      USE_CACHE(7) -> [4]
             * DO_CACHE(9) -> CACHE(6) + CACHE(7) = [3] + [4] = [7]
             *
             * DO_CACHE =  {(9,7)}
             * USE_CACHE = {(6,3),(7,4)}
             */
            var doCache = actualResultValue.DoCache;
            Assert.NotNull(doCache);
            Assert.Single(doCache);
            Assert.True(doCache[0].N == 9 && doCache[0].Fn == 7);

            var useCache = actualResultValue.UseCache;
            Assert.NotNull(useCache);
            Assert.Equal(2, useCache.Count);
            Assert.True(useCache[0].N == 6 && useCache[0].Fn == 3);
            Assert.True(useCache[1].N == 7 && useCache[1].Fn == 4);
        }
    }
}
