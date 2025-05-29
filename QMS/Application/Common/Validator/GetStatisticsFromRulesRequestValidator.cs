
using Application.Queries.GetStatisticsFromRules.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class GetStatisticsFromRulesRequestValidator : AbstractValidator<GetStatisticsFromRulesRequest>
    {
        public GetStatisticsFromRulesRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Fieldwork Id is required.")
                .GreaterThan(0).WithMessage("FieldWork Id must be greater than 0.");
        }
    }
}
