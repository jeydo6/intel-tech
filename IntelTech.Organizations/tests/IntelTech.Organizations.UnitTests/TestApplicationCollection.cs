using IntelTech.Common.Testing;
using IntelTech.Organizations.Presentation;
using Xunit;

namespace IntelTech.Organizations.UnitTests;

[CollectionDefinition(Collection)]
public abstract class TestApplicationCollection : ICollectionFixture<TestApplicationFixture<Program>>
{
    public const string Collection = nameof(TestApplicationCollection);
}
