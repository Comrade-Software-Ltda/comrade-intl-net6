using Comrade.Application.Bases;
using Comrade.Application.Lookups;
using Comrade.UnitTests.DataInjectors;
using Xunit;
using Xunit.Abstractions;

namespace Comrade.IntegrationTests.Tests.LookupIntegrationTests;

public sealed class CommonControllerLookupSystemUserTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;
    private readonly ITestOutputHelper _output;

    public CommonControllerLookupSystemUserTests(ITestOutputHelper output,
        ServiceProviderFixture fixture)
    {
        _output = output;
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task GetLookupSystemUser_Test()
    {
        var commonController =
            CommonControllerInjection.GetCommonController(_fixture.SqlContextFixture,
                _fixture.MongoDbContextFixture, _fixture.Sp);
        var result = await commonController.GetLookupSystemUser();

        if (result is ObjectResult okResult)
        {
            var actualResultValue = okResult.Value as ListResultDto<LookupDto>;
            Assert.NotNull(actualResultValue);
            _output.WriteLine(actualResultValue?.Code.ToString());
            Assert.NotNull(actualResultValue?.Data);
            Assert.Equal(4, actualResultValue?.Data?.Count);
        }
    }
}