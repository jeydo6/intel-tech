namespace IntelTech.Organizations.Application.Extensions;

internal static class MappingExtension
{
    public static Commands.CreateUserCommand Map(this Messages.CreateUserMessage source)
        => new Commands.CreateUserCommand
        {
            FirstName = source.FirstName,
            MiddleName = source.MiddleName,
            LastName = source.LastName,
            PhoneNumber = source.PhoneNumber,
            Email = source.Email
        };

    public static Domain.Models.PaginationInfo Map(this Models.PaginationInfo source)
        => new Domain.Models.PaginationInfo
        {
            Limit = (source.PageNumber - 1) * source.PageSize,
            Offset = source.PageSize
        };

    public static Models.User Map(this Domain.Entities.User source)
        => new Models.User
        {
            Id = source.Id,
            FirstName = source.FirstName,
            MiddleName = source.MiddleName,
            LastName = source.LastName,
            PhoneNumber = source.PhoneNumber,
            Email = source.Email,
            OrganizationId = source.OrganizationId
        };
}
