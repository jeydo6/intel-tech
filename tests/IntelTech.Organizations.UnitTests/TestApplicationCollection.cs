using Xunit;

namespace IntelTech.Organizations.UnitTests;

[CollectionDefinition(Collection)]
public sealed class TestApplicationCollection : ICollectionFixture<TestApplicationFixture>
{
    public const string Collection = nameof(TestApplicationCollection);
}
