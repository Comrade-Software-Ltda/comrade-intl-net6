#region

using Xunit;

#endregion

namespace Comrade.ComponentTests;

[CollectionDefinition("Api Collection")]
public sealed class
        CustomWebApplicationFactoryCollection : ICollectionFixture<
            CustomWebApplicationFactoryFixture>
{
}
