using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IntelTech.Users.Presentation;

public sealed class Program
{
    public static Task Main(string[] args)
        => CreateHostBuilder(args)
            .Build()
            .RunAsync();

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
}
