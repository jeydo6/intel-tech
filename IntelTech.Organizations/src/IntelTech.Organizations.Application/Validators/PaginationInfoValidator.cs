using FluentValidation;
using IntelTech.Organizations.Application.Models;

namespace IntelTech.Organizations.Application.Validators
{
    internal sealed class PaginationInfoValidator : AbstractValidator<PaginationInfo>
    {
        public PaginationInfoValidator()
        {
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.PageNumber).GreaterThan(0);
        }
    }
}
