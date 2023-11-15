using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Application.Commands;
using IntelTech.Organizations.Domain.Repositories;
using MediatR;

namespace IntelTech.Organizations.Application
{
    internal sealed class AddOrganizationUserHandler : IRequestHandler<AddOrganizationUserCommand>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public AddOrganizationUserHandler(IOrganizationRepository organizationRepository)
            => _organizationRepository = organizationRepository;

        public async Task<Unit> Handle(AddOrganizationUserCommand request, CancellationToken cancellationToken)
        {
            await _organizationRepository.AddUser(request.OrganizationId, request.UserId, cancellationToken);
            return Unit.Value;
        }
    }
}
