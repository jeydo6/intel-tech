using FluentValidation;
using IntelTech.Common.Mediator.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Common.Mediator.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMediator<T>(this IServiceCollection services)
            => services
                .AddMediatR(cfg => cfg
                    .RegisterServicesFromAssemblyContaining<T>()
                    .AddOpenBehavior(typeof(ValidationBehavior<,>)))
                .AddValidatorsFromAssemblyContaining<T>();
    }
}
