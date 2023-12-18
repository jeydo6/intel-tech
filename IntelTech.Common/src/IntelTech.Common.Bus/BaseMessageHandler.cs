using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IntelTech.Common.Bus;

public abstract class BaseMessageHandler<TMessage, TRequest> : IMessageHandler<TMessage>
    where TMessage : notnull
    where TRequest : IBaseRequest
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    protected BaseMessageHandler(
        IMediator mediator,
        ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Handle(TMessage message, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Сообщение {@Message} успешно получено", message);
        await _mediator.Send(Map(message), cancellationToken);
        _logger.LogInformation("Сообщение {@Message} успешно обработано", message);
    }

    protected abstract TRequest Map(TMessage message);
}
