using IntelTech.Organizations.Application.Models;
using MediatR;

namespace IntelTech.Organizations.Application.Queries
{
    public sealed class GetOrganizationUsersQuery : IRequest<User[]>
    {
        public int? OrganizationId { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
