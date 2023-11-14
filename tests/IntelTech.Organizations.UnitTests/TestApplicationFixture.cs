using System;
using IntelTech.Organizations.Presentation;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Organizations.UnitTests;

public sealed class TestApplicationFixture : IDisposable
{
    public WebApplicationFactory<Program> Factory { get; } = new TestApplicationFactory<Program>();
    
    public WebApplicationFactory<Program> CreateHost() => CreateHost(_ => { });

    public WebApplicationFactory<Program> CreateHost(Action<IServiceCollection> dependencyOverrides)
        => Factory.WithWebHostBuilder(builder => builder.ConfigureServices(dependencyOverrides));

    public void Dispose() => Factory.Dispose();
}