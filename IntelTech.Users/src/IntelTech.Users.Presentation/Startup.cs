using IntelTech.Common.Bus.RabbitMQ.Extensions;
using IntelTech.Common.Mediator.Extensions;
using IntelTech.Users.Presentation.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddRabbitMqBus<Application.AssemblyMarker>(Configuration);
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
