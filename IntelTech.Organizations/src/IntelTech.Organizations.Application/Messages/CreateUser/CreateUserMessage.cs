using MassTransit;

namespace IntelTech.Organizations.Application.Messages;

[MessageUrn(nameof(CreateUserMessage))]
public sealed class CreateUserMessage
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
