using System.Threading.Tasks;
using IntelTech.Common.Bus.Messages;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Domain.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IntelTech.Organizations.Application.Consumers
{
    public sealed class CreateUserConsumer : IConsumer<CreateUserMessage>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CreateUserConsumer> _logger;

        public CreateUserConsumer(
            IUserRepository userRepository,
            ILogger<CreateUserConsumer> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CreateUserMessage> context)
        {
            _logger.LogInformation("Сообщение {@Message} успешно получено", context.Message);

            var userId = await _userRepository.Create(new User
            {
                FirstName = context.Message.FirstName,
                MiddleName = context.Message.MiddleName,
                LastName = context.Message.LastName,
                PhoneNumber = context.Message.PhoneNumber,
                Email = context.Message.Email,
            }, context.CancellationToken);

            _logger.LogInformation("Пользователь с идентификатором '{UserId}' успешно создан", userId);
        }
    }
}
