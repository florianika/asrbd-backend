using Application.FieldWorkRule.RemoveFieldWorkRule.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class RemoveFieldWorkRuleRequestValidator : AbstractValidator<RemoveFieldWorkRuleRequest>
    {
        public RemoveFieldWorkRuleRequestValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Fieldwork Id is required.")
                .GreaterThan(0).WithMessage("Fieldwork Id must be greater than 0.");
            RuleFor(x => x.RuleId)
               .NotEmpty().WithMessage("Rule Id is required.")
               .GreaterThan(0).WithMessage("Rule Id must be greater than 0.");
        }
    }
}
