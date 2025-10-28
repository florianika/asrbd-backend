using FluentValidation;

namespace Application.User.ForgetPassword.Request
{
    public class ForgetPasswordRequest : User.Request
    {
        public required string  Email { get; set; }
    }
    public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
    {
        public ForgetPasswordRequestValidator()
        {
            RuleFor(cur => cur.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Invalid email address format");           
        }
    }
}
