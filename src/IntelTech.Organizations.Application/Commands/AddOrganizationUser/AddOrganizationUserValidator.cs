using FluentValidation;

namespace IntelTech.Organizations.Application.Commands;

public sealed class AddOrganizationUserValidator : AbstractValidator<AddOrganizationUserCommand>
{
    public AddOrganizationUserValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.OrganizationId).GreaterThan(0);
    }
}
