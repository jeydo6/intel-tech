using FluentValidation;
using IntelTech.Organizations.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Organizations.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblyContaining<Application.AssemblyMarker>()
            .AddOpenBehavior(typeof(ValidationBehavior<,>)));
        services.AddValidatorsFromAssemblyContaining<Application.AssemblyMarker>();

        return services;
    }
}
