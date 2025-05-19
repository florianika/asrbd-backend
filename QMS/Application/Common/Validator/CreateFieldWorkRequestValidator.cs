using Application.FieldWork.CreateFieldWork.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class CreateFieldWorkRequestValidator : AbstractValidator<CreateFieldWorkRequest>
    {
        public CreateFieldWorkRequestValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.");

            RuleFor(x => x.CreatedUser)
                .NotEqual(Guid.Empty).WithMessage("CreatedUser is required.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description can't exceed 1000 characters.");
        }
    }
}
