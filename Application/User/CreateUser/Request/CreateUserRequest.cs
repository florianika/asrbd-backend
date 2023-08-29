using Domain.Claim;
using FluentValidation;

namespace Application.User.CreateUser.Request
{
    public class CreateUserRequest : User.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public IList<Claim>? Claims { get; set; }
    }

      public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(cur => cur.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Invalid email address format");
            RuleFor(cur => cur.Name).NotNull().MinimumLength(2).WithMessage("Invalid Name");
            RuleFor(cur => cur.LastName).NotEmpty().NotNull().MinimumLength(2).WithMessage("Invalid last name");
        }
    }
}
