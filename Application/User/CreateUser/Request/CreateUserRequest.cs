using Domain;
using FluentValidation;

namespace Application.User.CreateUser.Request
{
    #nullable disable
    public class CreateUserRequest : User.Request
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public District? District { get; set; }
        public IList<Claim> Claims { get; set; }
    }

      public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(cur => cur.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Invalid email address format");
            RuleFor(cur => cur.Name).NotNull().MinimumLength(2).WithMessage("Invalid Name");
            RuleFor(cur => cur.LastName).NotEmpty().NotNull().MinimumLength(2).WithMessage("Invalid last name");
            RuleFor(cur => cur.Password).NotEmpty().NotNull().MinimumLength(6).WithMessage("Invalid Password");
            RuleFor(cur => cur.District).Cascade(CascadeMode.Stop)
            .Must(d => d == null || (d.Code > 0 && !string.IsNullOrWhiteSpace(d.Value)))
            .WithMessage("Invalid district information"); //District mund të mungojë, por nëse jepet, duhet të ketë Code > 0 dhe Value jo bosh.
        }
    }
}
