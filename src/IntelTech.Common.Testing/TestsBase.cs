using System;
using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Common.Testing;

public abstract class TestsBase<TEntryPoint> : IDisposable
    where TEntryPoint : class
{
    protected readonly Faker Faker = new Faker("ru");

    private readonly TestApplicationFixture<TEntryPoint> _fixture;

    public TestsBase(TestApplicationFixture<TEntryPoint> fixture)
        => _fixture = fixture;

    protected WebApplicationFactory<TEntryPoint> CreateHost() => CreateHost(_ => { });

    protected WebApplicationFactory<TEntryPoint> CreateHost(Action<IServiceCollection> dependencyOverrides)
        => _fixture.CreateHost(dependencyOverrides);

    public void Dispose() => _fixture.Dispose();
}
