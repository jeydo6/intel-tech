using FluentValidation;
using IntelTech.Bus.Domain.Settings;
using IntelTech.Organizations.Application.Behaviors;
using IntelTech.Organizations.Domain.Repositories;
using IntelTech.Organizations.Infrastructure.DbContexts;
using IntelTech.Organizations.Infrastructure.Repositories;
using IntelTech.Organizations.Presentation.Filters;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace IntelTech.Organizations.Presentation
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BusSettings>(Configuration.GetSection(nameof(BusSettings)));

            services.AddControllers(c =>
            {
                c.Filters.Add<ValidationExceptionFilter>();
                c.Filters.Add<ApplicationExceptionFilter>();
            });
            services.AddSwaggerGen();

            services.AddAutoMapper(typeof(Startup));
            services
                .AddMediatR(typeof(Application.AssemblyMarker))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<Application.AssemblyMarker>();

            services.Configure<BusSettings>(Configuration.GetSection(nameof(BusSettings)));

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumersFromNamespaceContaining<Application.AssemblyMarker>();
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
            services.AddMassTransitHostedService();

            services
                .AddDbContext<ApplicationDbContext>()
                .AddScoped<IOrganizationRepository, OrganizationRepository>()
                .AddScoped<IUserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
