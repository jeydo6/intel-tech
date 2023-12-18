using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Domain.Entities;
using IntelTech.Organizations.Domain.Repositories;
using MediatR;

namespace IntelTech.Organizations.Application.Commands;

internal sealed class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
        => _userRepository = userRepository;

    public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        => _userRepository.Create(new User
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
        }, cancellationToken);
}
