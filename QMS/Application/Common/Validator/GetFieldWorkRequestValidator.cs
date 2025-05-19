using Application.FieldWork.GetFieldWork.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class GetFieldWorkRequestValidator : AbstractValidator<GetFieldWorkRequest>
    {
        public GetFieldWorkRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
