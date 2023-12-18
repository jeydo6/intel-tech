using IntelTech.Common.Bus;
using IntelTech.Organizations.Application.Commands;
using IntelTech.Organizations.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace IntelTech.Organizations.Application.Messages;

internal sealed class CreateUserHandler : BaseMessageHandler<CreateUserMessage, CreateUserCommand>
{
    public CreateUserHandler(
        IMediator mediator,
        ILogger<CreateUserHandler> logger) : base(mediator, logger)
    {
    }

    protected override CreateUserCommand Map(CreateUserMessage message)
        => message.Map();
}
