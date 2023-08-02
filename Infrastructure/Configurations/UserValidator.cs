using FluentValidation;
using Domain.User;

namespace Infrastructure.Configurations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Invalid email address format");
        }
    }
}
