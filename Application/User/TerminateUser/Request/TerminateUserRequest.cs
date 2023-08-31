
using FluentValidation;

namespace Application.User.TerminateUser.Request
{
    public class TerminateUserRequest : User.Request
    {
        public Guid UserId { get; set; }
    }

    public class TerminateUserRequestValidator : AbstractValidator<TerminateUserRequest>
    {
        public TerminateUserRequestValidator() 
        {
            RuleFor(tu => tu.UserId).NotEmpty().NotNull();
        }
    }
}
