using System;
//using Comrade.Application.Bases;
using Comrade.Application.Caches;
//using Comrade.UnitTests.DataInjectors;
//using Comrade.UnitTests.Tests.AirplaneTests.Bases;
using StackExchange.Redis;
using Xunit;

namespace Comrade.IntegrationTests.Tests.FunctionIntegrationTests;

public class FunctionControllerAlticciTest : IClassFixture<ServiceProviderFixture>
{
    //private readonly ServiceProviderFixture _fixture;
    //private readonly RedisCacheService _redisCacheService;
    static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
        new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" }
        });
    public FunctionControllerAlticciTest()//ServiceProviderFixture fixture, RedisCacheService redisCacheService)
    {
        //_fixture = fixture;
        //InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
        //_redisCacheService = redisCacheService;
    }

    [Fact]
    public async Task FunctionController_AlticciAsync()
    {
        var db = redis.GetDatabase();
        var pong = await db.PingAsync();
        Console.WriteLine(pong);
        //var functionController = FunctionInjectionController.GetFunctionController(_redisCacheService);

        //var result = functionController.Alticci(10);
        //if (result is ObjectResult okResult)
        //{
        //    var actualResultValue = okResult.Value as SingleResultDto<EntityDto>;
        //    Assert.NotNull(actualResultValue);
        //    Assert.Equal(9, actualResultValue?.Code);
        //}
    }
}
