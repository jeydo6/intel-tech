using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IntelTech.Organizations.Application.Extensions;
using IntelTech.Organizations.Application.Models;
using IntelTech.Organizations.Domain.Repositories;
using MediatR;

namespace IntelTech.Organizations.Application.Queries
{
    internal sealed class GetOrganizationUsersHandler : IRequestHandler<GetOrganizationUsersQuery, User[]>
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationUsersHandler(
            IOrganizationRepository organizationRepository
        )
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<User[]> Handle(GetOrganizationUsersQuery request, CancellationToken cancellationToken)
        {
            var paginationInfo = request.PaginationInfo.Map();
            var userEntities = await _organizationRepository.GetUsers(request.OrganizationId, paginationInfo, cancellationToken);

            return userEntities
                .Select(MappingExtension.Map)
                .ToArray();
        }
    }
}
