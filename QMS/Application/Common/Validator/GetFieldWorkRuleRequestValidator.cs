using Application.FieldWorkRule.GetFieldWorkRule.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class GetFieldWorkRuleRequestValidator : AbstractValidator<GetFieldWorkRuleRequest>
    {
        public GetFieldWorkRuleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
