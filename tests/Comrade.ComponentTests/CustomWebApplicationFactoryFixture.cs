using Comrade.UnitTests.Helpers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Comrade.ComponentTests;

/// <summary>
/// </summary>
public sealed class CustomWebApplicationFactoryFixture : IDisposable
{
    public CustomWebApplicationFactoryFixture()
    {
        var serviceCollection = GetServiceCollection.Execute();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .Build();

        serviceCollection.AddSingleton<IConfiguration>(configuration);
        Mediator = serviceCollection.BuildServiceProvider().GetRequiredService<IMediator>();
        CustomWebApplicationFactory = new CustomWebApplicationFactory();
    }

    public IMediator Mediator { get; }
    public CustomWebApplicationFactory CustomWebApplicationFactory { get; }

    public void Dispose()
    {
    }
}
