using Application.FieldWorkRule.GetStatisticsByRule.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class GetStatisticsByRuleRequestValidator : AbstractValidator<GetStatisticsByRuleRequest>
    {
        public GetStatisticsByRuleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Fieldwork Id is required.")
                .GreaterThan(0).WithMessage("FieldWork Id must be greater than 0.");
        }
    }
}
