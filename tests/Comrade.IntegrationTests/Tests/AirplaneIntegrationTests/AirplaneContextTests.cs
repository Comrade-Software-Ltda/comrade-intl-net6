using System;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
        InjectDataOnContextBase.InitializeDbForTests(_fixture.SqlContextFixture);
    }

    [Fact]
    public async Task Airplane_Context()
    {
        var airplaneId = new Guid("063f44b8-df8b-4f96-889a-75b9d67c546f");
        var repository = new AirplaneRepository(_fixture.SqlContextFixture);
        var airplane = await repository.GetById(airplaneId);
        Assert.NotNull(airplane);
    }
}
