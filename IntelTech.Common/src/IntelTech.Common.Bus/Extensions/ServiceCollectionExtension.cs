using Microsoft.Extensions.DependencyInjection;

namespace IntelTech.Common.Bus.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMessageHandlersFromAssemblyContaining<T>(this IServiceCollection services)
    {
        foreach (var implementationType in typeof(T).Assembly.GetTypes())
        {
            if (!implementationType.IsClass || implementationType.IsAbstract)
                continue;

            foreach (var serviceType in implementationType.GetInterfaces())
            {
                if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IMessageHandler<>))
                {
                    services.AddTransient(serviceType, implementationType);
                    services.AddTransient(typeof(IMessageHandler), implementationType);
                    break;
                }
            }
        }

        return services;
    }
}
