using Comrade.Persistence.DataAccess;
using Comrade.Persistence.Repositories;
using Comrade.UnitTests.DataInjectors;
using MediatR;
using Xunit;

namespace Comrade.IntegrationTests.Tests.AirplaneIntegrationTests;

public class AirplaneContextTests : IClassFixture<ServiceProviderFixture>
{
    private readonly ServiceProviderFixture _fixture;

    public AirplaneContextTests(ServiceProviderFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Airplane_Context()
    {
        var sp = _fixture.InitiateConxtext("test_database_Airplane_Context");
        var mediator = sp.GetRequiredService<IMediator>();
        var context = sp.GetService<ComradeContext>()!;
        InjectDataOnContextBase.InitializeDbForTests(context);
        var repository = new AirplaneRepository(context);
        var airplane = await repository.GetById(1);
        Assert.NotNull(airplane);
    }
}