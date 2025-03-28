using FluentValidation;

namespace Application.User.SetUserMunicipality.Request;
#nullable disable
public class SetUserMunicipalityRequest : User.Request
{
    public Guid UserId { get; set; }
    public string MunicipalityCode { get; set; }
    
}

public class SetUserMunicipalityRequestValidator : AbstractValidator<SetUserMunicipalityRequest>
{
    public SetUserMunicipalityRequestValidator() 
    {
        RuleFor(sum => sum.MunicipalityCode).NotEmpty().NotNull();
        RuleFor(sum => sum.UserId).NotEmpty().NotNull();
    }
    
    
}