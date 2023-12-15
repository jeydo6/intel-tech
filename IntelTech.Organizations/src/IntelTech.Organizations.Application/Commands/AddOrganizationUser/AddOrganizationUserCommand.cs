using MediatR;

namespace IntelTech.Organizations.Application.Commands
{
    public sealed class AddOrganizationUserCommand : IRequest
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
    }
}
