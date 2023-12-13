using System.Threading.Tasks;
using IntelTech.Common.Migrations.Migrators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Organizations.Infrastructure.Extensions
{
    public static class HostExtension
    {
        public static async Task RunWithMigrate<TDbContext>(this IHost host)
            where TDbContext : DbContext
        {
            using var scope = host.Services.CreateScope();
            var migrator = scope.ServiceProvider.GetRequiredService<IDbMigrator>();

            await migrator.MigrateAsync();
            await host.RunAsync();
        }
    }
}
