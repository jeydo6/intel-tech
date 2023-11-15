using System;
using System.Threading;
using System.Threading.Tasks;
using IntelTech.Bus.Domain.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IntelTech.Users.Application.Commands;

internal sealed class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IBus _bus;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(
        IBus bus,
        ILogger<CreateUserHandler> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var message = new CreateUserMessage
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        try
        {
            await _bus.Publish(message, cancellationToken);
            _logger.LogInformation("Сообщение {@Message} успешно отправлено", message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "При отправке сообщения {@Message} произошла ошибка", message);
            throw;
        }
    }
}
