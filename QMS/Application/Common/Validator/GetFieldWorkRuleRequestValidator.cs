using Application.FieldWorkRule.GetFieldWorkRule.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class GetFieldWorkRuleRequestValidator : AbstractValidator<GetFieldWorkRuleRequest>
    {
        public GetFieldWorkRuleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Fieldwork Id is required.")
                .GreaterThan(0).WithMessage("FieldWork Id must be greater than 0.");

            RuleFor(x => x.RuleId)
                .NotEmpty().WithMessage("Rule Id is required.")
                .GreaterThan(0).WithMessage("Rule Id must be greater than 0.");
        }
    }
}
