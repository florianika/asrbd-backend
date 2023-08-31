
using FluentValidation;

namespace Application.User.RefreshToken.Request
{
    #nullable disable
    public class RefreshTokenRequest : User.Request
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator() 
        {
            RuleFor(rt => rt.AccessToken).NotEmpty().NotNull();
            RuleFor(rt => rt.RefreshToken).NotEmpty().NotNull();
        }
    }
}
