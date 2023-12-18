namespace IntelTech.Users.Presentation.Extensions;

internal static class MappingExtension
{
    public static Application.Commands.CreateUserCommand Map(this Contracts.CreateUserRequest source)
        => new Application.Commands.CreateUserCommand
        {
            FirstName = source.FirstName,
            MiddleName = source.MiddleName,
            LastName = source.LastName,
            PhoneNumber = source.PhoneNumber,
            Email = source.Email
        };
}
