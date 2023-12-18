using System;
using System.Threading;
using System.Threading.Tasks;
using IntelTech.Common.Bus;
using IntelTech.Users.Application.Messages;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IntelTech.Users.Application.Commands
{
    internal sealed class CreateUserHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IMessageProducer _producer;
        private readonly ILogger<CreateUserHandler> _logger;

        public CreateUserHandler(
            IMessageProducer producer,
            ILogger<CreateUserHandler> logger)
        {
            _producer = producer;
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
                await _producer.Produce(message, cancellationToken);
                _logger.LogInformation("Сообщение {@Message} успешно отправлено", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "При отправке сообщения {@Message} произошла ошибка", message);
                throw;
            }
        }
    }
}
