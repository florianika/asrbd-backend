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
        public string MunicipalityCode { get; set; }
    }

      public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(cur => cur.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Invalid email address format");
            RuleFor(cur => cur.Name).NotNull().MinimumLength(2).WithMessage("Invalid Name");
            RuleFor(cur => cur.LastName).NotEmpty().NotNull().MinimumLength(2).WithMessage("Invalid last name");
            RuleFor(cur => cur.Password).NotEmpty().NotNull().MinimumLength(6).WithMessage("Invalid Password");
            //Claims should contain at least the municipality
            RuleFor(cur => cur.MunicipalityCode).NotEmpty().NotNull().MinimumLength(1).MaximumLength(2)
                .Must(code => int.TryParse(code, out var result) && result is >= 1 and <= 61)
                .WithMessage("Invalid Municipality Code");
        }
    }
}
