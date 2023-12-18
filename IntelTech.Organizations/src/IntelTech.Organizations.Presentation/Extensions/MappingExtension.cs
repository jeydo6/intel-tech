namespace IntelTech.Organizations.Presentation.Extensions;

internal static class MappingExtension
{
    public static Application.Commands.AddOrganizationUserCommand Map(this Contracts.AddOrganizationUserRequest source)
        => new Application.Commands.AddOrganizationUserCommand
        {
            UserId = source.UserId,
            OrganizationId = source.OrganizationId
        };

    public static Application.Queries.GetOrganizationUsersQuery Map(this Contracts.GetOrganizationUsersRequest source)
        => new Application.Queries.GetOrganizationUsersQuery
        {
            OrganizationId = source.OrganizationId,
            PaginationInfo = source.PaginationInfo
        };
}
