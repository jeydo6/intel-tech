using Xunit;

namespace IntelTech.Organizations.UnitTests;

[CollectionDefinition(Collection)]
public class TestApplicationCollection : ICollectionFixture<TestApplicationFixture>
{
    public const string Collection = nameof(TestApplicationCollection);
}
