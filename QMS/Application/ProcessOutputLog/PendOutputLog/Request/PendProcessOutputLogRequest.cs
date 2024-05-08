using System;
using FluentValidation;

namespace Application.ProcessOutputLog.PendOutputLog.Request
{
    public class PendProcessOutputLogRequest : ProcessOutputLog.Request
    {
        public Guid ProcessOutputLogId { get; set; }
        public Guid UpdatedUser { get; set; }        
    }

    public class PendProcessOutputLogRequestValidator : AbstractValidator<PendProcessOutputLogRequest>
{
    public PendProcessOutputLogRequestValidator()
    {
        RuleFor(r => r.ProcessOutputLogId).NotNull().NotEmpty()
            .WithMessage("Process output log id should not be empty");
    }
}
}
