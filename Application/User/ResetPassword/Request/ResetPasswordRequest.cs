using FluentValidation;

namespace Application.User.ResetPassword.Request
{
    public class ResetPasswordRequest : User.Request
    {
        public required string Token { get; set; }
        public required string NewPassword { get; set; }
    }
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(cur => cur.NewPassword).NotEmpty().NotNull().MinimumLength(6).WithMessage("Invalid Password");
        }
    }
}
