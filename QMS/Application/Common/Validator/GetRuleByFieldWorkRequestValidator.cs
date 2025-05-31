
using Application.FieldWorkRule.GetRuleByFieldWork.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class GetRuleByFieldWorkRequestValidator : AbstractValidator<GetRuleByFieldWorkRequest>
    {
        public GetRuleByFieldWorkRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Fieldwork Id is required.")
                .GreaterThan(0).WithMessage("FieldWork Id must be greater than 0.");
        }
    }
}
