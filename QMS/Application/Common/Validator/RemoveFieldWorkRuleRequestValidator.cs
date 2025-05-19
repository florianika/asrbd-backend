using Application.FieldWorkRule.RemoveFieldWorkRule.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class RemoveFieldWorkRuleRequestValidator : AbstractValidator<RemoveFieldWorkRuleRequest>
    {
        public RemoveFieldWorkRuleRequestValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
