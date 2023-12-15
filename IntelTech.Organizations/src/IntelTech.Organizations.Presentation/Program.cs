using System.Threading.Tasks;
using IntelTech.Organizations.Infrastructure.DbContexts;
using IntelTech.Organizations.Infrastructure.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Organizations.Presentation
{
    public class Program
    {
        public static Task Main(string[] args)
            => CreateHostBuilder(args)
                .Build()
                .RunWithMigrate<ApplicationDbContext>();

        private static IHostBuilder CreateHostBuilder(string[] args)
            => Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}
