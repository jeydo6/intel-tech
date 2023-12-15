using IntelTech.Organizations.Application.Models;

namespace IntelTech.Organizations.Presentation.Contracts
{
    public sealed class GetOrganizationUsersRequest
    {
        public int? OrganizationId { get; set; }
        public required PaginationInfo PaginationInfo { get; set; }
    }
}
