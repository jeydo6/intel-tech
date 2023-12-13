using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Common.Testing;

public sealed class TestApplicationFixture<TEntryPoint> : IDisposable
    where TEntryPoint : class
{
    public WebApplicationFactory<TEntryPoint> Factory { get; } = new TestApplicationFactory<TEntryPoint>();

    public WebApplicationFactory<TEntryPoint> CreateHost() => CreateHost(_ => { });

    public WebApplicationFactory<TEntryPoint> CreateHost(Action<IServiceCollection> dependencyOverrides)
        => Factory.WithWebHostBuilder(builder => builder.ConfigureServices(dependencyOverrides));

    public void Dispose() => Factory.Dispose();
}
