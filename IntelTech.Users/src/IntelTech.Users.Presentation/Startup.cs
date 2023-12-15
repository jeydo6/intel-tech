using IntelTech.Common.Bus.Settings;
using IntelTech.Common.Mediator.Extensions;
using IntelTech.Users.Presentation.Filters;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;

namespace IntelTech.Users.Presentation
{
    internal class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BusSettings>(Configuration.GetSection(nameof(BusSettings)));

            services.AddControllers(c =>
            {
                c.Filters.Add<ValidationExceptionFilter>();
                c.Filters.Add<ApplicationExceptionFilter>();
            });
            services.AddSwaggerGen();

            services.AddSerilog(cfg => cfg
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
            );

            services.AddAutoMapper(typeof(Startup));
            services.AddMediator<Application.AssemblyMarker>();

            services.AddMassTransit(cfg =>
            {
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
