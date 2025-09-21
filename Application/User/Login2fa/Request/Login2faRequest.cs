using Application.User.Login.Request;
using FluentValidation;

namespace Application.User.Login2fa.Request
{
    public class Login2faRequest : User.Request
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class Login2faRequestValidator : AbstractValidator<Login2faRequest>
    {
        public Login2faRequestValidator()
        {
            RuleFor(lr => lr.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email not valid");
            RuleFor(lr => lr.Password).NotEmpty().NotNull().MinimumLength(6);
        }
    }
}
