using IntelTech.Organizations.Application.Extensions;
using IntelTech.Organizations.Infrastructure.Extensions;
using IntelTech.Organizations.Presentation.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace IntelTech.Organizations.Presentation;

internal class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(c =>
        {
            c.Filters.Add<ValidationExceptionFilter>();
            c.Filters.Add<ApplicationExceptionFilter>();
        });
        services.AddSwaggerGen();
        services.AddAutoMapper(typeof(Startup));

        services.AddSerilog(cfg => cfg
            .ReadFrom.Configuration(Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console()
        );
        services.AddApplication();
        services.AddInfrastructure(Configuration);
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
