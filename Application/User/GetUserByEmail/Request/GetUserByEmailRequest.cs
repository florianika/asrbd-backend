using FluentValidation;

namespace Application.User.GetUserByEmail.Request;

#nullable disable
public class GetUserByEmailRequest : User.Request
{
    public string Email { get; set; }
    
}

public class GetUserRequestByEmailValidator : AbstractValidator<GetUserByEmailRequest>
{
    public GetUserRequestByEmailValidator() 
    {
        RuleFor(u => u.Email).NotEmpty().NotNull();
    }
}