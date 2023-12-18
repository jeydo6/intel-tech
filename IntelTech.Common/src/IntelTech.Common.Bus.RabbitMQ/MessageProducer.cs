using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using IntelTech.Common.Bus.RabbitMQ.Extensions;

namespace IntelTech.Common.Bus.RabbitMQ;

public sealed class MessageProducer : IMessageProducer
{
    private readonly IBus _bus;
    private readonly ILogger<MessageProducer> _logger;

    public MessageProducer(
        IBus bus,
        ILogger<MessageProducer> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task Produce<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : notnull
    {
        await _bus.Send(message, cancellationToken);
        _logger.LogTrace("Сообщение {@Message} успешно отправлено", message);
    }
}
