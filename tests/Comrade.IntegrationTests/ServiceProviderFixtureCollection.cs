using Xunit;

namespace Comrade.IntegrationTests;

[CollectionDefinition(nameof(ServiceProviderFixture))]
public class ServiceProviderFixtureCollection : ICollectionFixture<ServiceProviderFixture>
{
}
