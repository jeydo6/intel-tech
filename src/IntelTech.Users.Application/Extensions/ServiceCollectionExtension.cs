using FluentValidation;
using IntelTech.Users.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Users.Application.Extensions;

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
