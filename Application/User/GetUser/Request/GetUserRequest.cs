
using FluentValidation;

namespace Application.User.GetUser.Request
{
    public class GetUserRequest : User.Request
    {
        public Guid UserId { get; set; }
    }

    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        public GetUserRequestValidator() 
        {
            RuleFor(gu => gu.UserId).NotEmpty().NotNull();
        }
    }
}
