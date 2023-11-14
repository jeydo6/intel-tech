using System.Linq;
using System.Threading.Tasks;
using IntelTech.Organizations.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Organizations.Infrastructure.Extensions
{
    public static class HostExtension
    {
        public static async Task RunWithMigrate(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var migrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (migrations.Any())
                await dbContext.Database.MigrateAsync();

            await host.RunAsync();
        }
    }
}
