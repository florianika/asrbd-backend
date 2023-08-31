
using FluentValidation;

namespace Application.User.ActivateUser.Request
{
    public class ActivateUserRequest : User.Request
    {
        public Guid UserId { get; set; }
    }

    public class ActivateUserRequestValidator : AbstractValidator<ActivateUserRequest>
    {
        public ActivateUserRequestValidator() 
        {
            RuleFor(au => au.UserId).NotEmpty().NotNull();
        }
    }
}
