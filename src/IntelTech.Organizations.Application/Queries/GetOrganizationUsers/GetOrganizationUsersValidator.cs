using FluentValidation;
using IntelTech.Organizations.Application.Validators;

namespace IntelTech.Organizations.Application.Queries
{
    public sealed class GetOrganizationUsersValidator : AbstractValidator<GetOrganizationUsersQuery>
    {
        public GetOrganizationUsersValidator()
        {
            RuleFor(x => x.OrganizationId).GreaterThan(0).When(x => x.OrganizationId != null);
            RuleFor(x => x.PaginationInfo).NotNull().SetValidator(new PaginationInfoValidator());
        }
    }
}
