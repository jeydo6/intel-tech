using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using IntelTech.Organizations.Application.Models;
using IntelTech.Organizations.Domain.Repositories;
using MediatR;

namespace IntelTech.Organizations.Application.Queries;

internal sealed class GetOrganizationUsersHandler : IRequestHandler<GetOrganizationUsersQuery, User[]>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationUsersHandler(
        IOrganizationRepository organizationRepository,
        IMapper mapper
    )
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }

    public async Task<User[]> Handle(GetOrganizationUsersQuery request, CancellationToken cancellationToken)
    {
        var paginationInfo = _mapper.Map<Domain.Models.PaginationInfo>(request.PaginationInfo);
        var userEntities = await _organizationRepository.GetUsers(request.OrganizationId, paginationInfo, cancellationToken);

        return _mapper.Map<User[]>(userEntities);
    }
}
