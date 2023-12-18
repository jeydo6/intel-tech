using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace IntelTech.Common.Bus.RabbitMQ.Extensions;

internal static class SendEndpointProviderExtension
{
    private static Task<ISendEndpoint> GetSendEndpoint<TMessage>(this ISendEndpointProvider sendEndpointProvider)
        where TMessage: notnull
    {
        const string schema = "queue";

        return sendEndpointProvider.GetSendEndpoint(
            new Uri(schema + ':' + typeof(TMessage).Name)
        );
    }

    public static async Task Send<TMessage>(this IBus bus, TMessage message, CancellationToken cancellationToken = default)
        where TMessage: notnull
    {
        var endpoint = await bus.GetSendEndpoint<TMessage>();
        await endpoint.Send(message, cancellationToken);
    }
}
