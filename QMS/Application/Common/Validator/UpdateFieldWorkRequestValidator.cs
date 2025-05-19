using Application.FieldWork.UpdateFieldWork.Request;
using FluentValidation;

namespace Application.Common.Validator
{
    public class UpdateFieldWorkRequestValidator : AbstractValidator<UpdateFieldWorkRequest>
    {
        public UpdateFieldWorkRequestValidator()
        {
            RuleFor(x => x.FieldWorkId)
                .GreaterThan(0).WithMessage("FieldWorkId must be a positive number.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.");

            //RuleFor(x => x.EmailTemplateId)
            //    .GreaterThan(0).WithMessage("EmailTemplateId is required.");

            RuleFor(x => x.UpdatedUser)
                .NotEqual(Guid.Empty).WithMessage("UpdatedUser is required.");
        }
    }

}
