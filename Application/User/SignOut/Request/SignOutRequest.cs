using FluentValidation;

namespace Application.User.SignOut.Request
{
    public class SignOutRequest : User.Request
    {
        public Guid UserId { get; set; }
    }

    public class SignOutRequestValidator : AbstractValidator<SignOutRequest>
    {
        public SignOutRequestValidator() 
        {
            RuleFor(so => so.UserId).NotEmpty().NotNull();
        }
    }
}
