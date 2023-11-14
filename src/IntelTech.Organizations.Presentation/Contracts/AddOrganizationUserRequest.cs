namespace IntelTech.Organizations.Presentation.Contracts
{
    public sealed class AddOrganizationUserRequest
    {
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
    }
}
