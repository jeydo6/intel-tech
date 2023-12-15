using IntelTech.Common.Migrations.Migrators;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Common.Testing;

public sealed class TestApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
    where TEntryPoint : class
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);

        using var scope = host.Services.CreateScope();
        var migrator = scope.ServiceProvider.GetRequiredService<IDbMigrator>();
        migrator.Migrate();

        return host;
    }
}
