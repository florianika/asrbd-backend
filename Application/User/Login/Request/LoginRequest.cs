using FluentValidation;

namespace Application.User.Login.Request
{
    #nullable disable
    public class LoginRequest : User.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(lr => lr.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email not valid");
            RuleFor(lr => lr.Password).NotEmpty().NotNull().MinimumLength(6);
        }
    }
}
