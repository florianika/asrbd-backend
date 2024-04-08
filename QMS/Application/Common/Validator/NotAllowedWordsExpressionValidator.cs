using FluentValidation;
using FluentValidation.Validators;

namespace Application.Common.Validator;

public class NotAllowedWordsExpressionValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    private readonly List<string> _notAllowedInExpression;

    public NotAllowedWordsExpressionValidator(List<string> notAllowedInExpression)
    {
        _notAllowedInExpression = notAllowedInExpression;
    }
    
    public override string Name => "NotAllowedWordsExpressionValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        return _notAllowedInExpression.Any(s =>
            value != null && value.ToString()!.Contains(s, StringComparison.InvariantCultureIgnoreCase));
    }
    
    protected override string GetDefaultMessageTemplate(string errorCode)
        => "'{PropertyName}' must not contain malicious script.";
}