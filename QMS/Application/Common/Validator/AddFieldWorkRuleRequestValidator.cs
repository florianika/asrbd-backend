using FluentValidation;
using Application.FieldWorkRule.AddFieldWorkRule.Request;

namespace Application.Common.Validator
{
    public class AddFieldWorkRuleRequestValidator : AbstractValidator<AddFieldWorkRuleRequest>
    {
        public AddFieldWorkRuleRequestValidator()
        {
            RuleFor(x => x.FieldWorkId)
                .GreaterThan(0).WithMessage("FieldWorkId must be greater than 0.");

            RuleFor(x => x.RuleId)
                .GreaterThan(0).WithMessage("RuleId must be greater than 0.");

            RuleFor(x => x.CreatedUser)
                .NotEqual(Guid.Empty).WithMessage("CreatedUser is required.");
        }
    }
}
