using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IntelTech.Common.Bus.RabbitMQ;

public sealed class MessageConsumer<TMessage> : IConsumer<TMessage> where TMessage : class
{
    private readonly IMessageHandler<TMessage> _handler;
    private readonly ILogger<MessageConsumer<TMessage>> _logger;

    public MessageConsumer(
        IMessageHandler<TMessage> handler,
        ILogger<MessageConsumer<TMessage>> logger)
    {
        _handler = handler;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TMessage> context)
    {
        _logger.LogTrace("Сообщение {@Message} успешно получено", context.Message);
        await _handler.Handle(context.Message);
        _logger.LogTrace("Сообщение {@Message} успешно обработано", context.Message);
    }
}
