using FluentValidation;

namespace Application.ProcessOutputLog.ResolveProcessOutputLog.Request;

public class ResolveProcessOutputLogRequest : ProcessOutputLog.Request
{
    public Guid ProcessOutputLogId { get; set; }
    public Guid UpdatedUser { get; set; }
}

public class ResolveProcessOutputLogRequestValidator : AbstractValidator<ResolveProcessOutputLogRequest>
{
    public ResolveProcessOutputLogRequestValidator()
    {
        RuleFor(r => r.ProcessOutputLogId).NotNull().NotEmpty()
            .WithMessage("Process output log id should not be empty");
    }
}