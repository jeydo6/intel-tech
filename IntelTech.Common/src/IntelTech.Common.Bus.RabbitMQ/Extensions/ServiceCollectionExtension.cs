using IntelTech.Common.Bus.Extensions;
using IntelTech.Common.Bus.RabbitMQ.Settings;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IntelTech.Common.Bus.RabbitMQ.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRabbitMqBus<T>(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqBusSettings>(configuration.GetSection(nameof(RabbitMqBusSettings)));
        services.AddMessageHandlersFromAssemblyContaining<T>();

        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumersFromAssemblyContaining<T>();
            cfg.UsingRabbitMq((context, busCfg) =>
            {
                var settings = context.GetRequiredService<IOptions<RabbitMqBusSettings>>().Value;
                busCfg.Host(settings.Host, "/", hostCfg =>
                {
                    hostCfg.Username(settings.User);
                    hostCfg.Password(settings.Password);
                });

                busCfg.ConfigureConsumers(context);
                busCfg.ConfigureEndpoints(context);
            });
        });
        services.AddScoped<IMessageProducer, MessageProducer>();

        return services;
    }

    private static void AddConsumersFromAssemblyContaining<T>(this IRegistrationConfigurator configurator)
    {
        foreach (var type in typeof(T).Assembly.GetTypes())
        {
            if (!type.IsClass || type.IsAbstract)
                continue;

            foreach (var serviceType in type.GetInterfaces())
            {
                if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IMessageHandler<>))
                {
                    var messageType = serviceType.GetGenericArguments()[0];
                    var consumerType = typeof(MessageConsumer<>).MakeGenericType(messageType);

                    configurator.AddConsumer(consumerType);
                    break;
                }
            }
        }
    }

    private static void ConfigureConsumers(this IReceiveConfigurator configurator, IRegistrationContext context)
    {
        var messageHandlers = context.GetServices<IMessageHandler>();
        foreach (var messageHandler in messageHandlers)
        {
            foreach (var serviceType in messageHandler.GetType().GetInterfaces())
            {
                if (serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IMessageHandler<>))
                {
                    var messageType = serviceType.GetGenericArguments()[0];
                    var consumerType = typeof(MessageConsumer<>).MakeGenericType(messageType);

                    configurator.ReceiveEndpoint(messageType.Name, eCfg => eCfg.ConfigureConsumer(context, consumerType));
                    break;
                }
            }
        }
    }
}
