
using Application.FieldWork.OpenFieldWork.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class OpenFieldWorkRequestValidator : AbstractValidator<OpenFieldWorkRequest>
    {
        public OpenFieldWorkRequestValidator()
        {
            RuleFor(x => x.FieldWorkId)
                .GreaterThan(0)
                .WithMessage("FieldWorkId must be greater than 0");

            RuleFor(x => x.UpdatedUser)
                .NotEmpty()
                .WithMessage("UpdatedUser is required.");
        }
    }
}
