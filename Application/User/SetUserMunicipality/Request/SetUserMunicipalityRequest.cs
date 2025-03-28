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
        RuleFor(sum => sum.MunicipalityCode).NotEmpty().NotNull().MinimumLength(1).MaximumLength(2)
            .Must(code => int.TryParse(code, out var result) && result is >= 1 and <= 61)
            .WithMessage("Invalid Municipality Code");
        RuleFor(sum => sum.UserId).NotEmpty().NotNull();
    }
    
    
}