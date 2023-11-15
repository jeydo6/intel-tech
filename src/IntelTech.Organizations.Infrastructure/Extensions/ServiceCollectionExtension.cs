using IntelTech.Bus.Domain.Settings;
using IntelTech.Organizations.Domain.Repositories;
using IntelTech.Organizations.Infrastructure.DbContexts;
using IntelTech.Organizations.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IntelTech.Organizations.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BusSettings>(configuration.GetSection(nameof(BusSettings)));

        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumersFromNamespaceContaining<AssemblyMarker>();
            cfg.UsingRabbitMq((context, busCfg) =>
            {
                var busSettings = context.GetRequiredService<IOptions<BusSettings>>().Value;
                busCfg.Host(busSettings.Host, "/", hostCfg =>
                {
                    hostCfg.Username(busSettings.User);
                    hostCfg.Password(busSettings.Password);
                });

                busCfg.ConfigureEndpoints(context);
            });
        });

        services
            .AddDbContext<ApplicationDbContext>()
            .AddScoped<IOrganizationRepository, OrganizationRepository>()
            .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
