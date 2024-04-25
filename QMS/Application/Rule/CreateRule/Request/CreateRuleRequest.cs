using Application.Common.Validator;
using Domain.Enum;
using FluentValidation;

namespace Application.Rule.CreateRule.Request
{
    public class CreateRuleRequest : Rule.Request
    {
        public string LocalId { get; set; } = null!;
        public EntityType EntityType { get; set; }
        public string? Variable { get; set; }
        public string? NameAl { get; set; }
        public string? NameEn { get; set; }
        public string? DescriptionAl { get; set; }
        public string? DescriptionEn { get; set; }
        public string Version { get; set; } = "1.0";
        public string VersionRationale { get; set; } = "First Version";
        public string Expression { get; set; } = null!;
        public QualityAction QualityAction { get; set; }
        public RuleStatus RuleStatus { get; set; } = RuleStatus.ACTIVE;
        public string? RuleRequirement { get; set; }
        public string? Remark { get; set; }
        public string? QualityMessageAl { get; set; }
        public string? QualityMessageEn { get; set; }
        public Guid CreatedUser { get; set; }
    }

    public static class MyValidatorExtensions {
       public static IRuleBuilderOptions<T, TProperty> NotAllowedWordsExpressionValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, List<string> notAllowedWords) {
          return ruleBuilder.SetValidator(new NotAllowedWordsExpressionValidator<T, TProperty>(notAllowedWords));
       }
    }
    public class CreateRuleValidator : AbstractValidator<CreateRuleRequest>
    {
        public CreateRuleValidator()
        {
            RuleFor(rl => rl.Variable).NotEmpty().NotNull().WithMessage("Variable should be not null");
            RuleFor(rl => rl.Version).NotEmpty().NotNull().WithMessage("Version should be not null");
            RuleFor(rl => rl.Expression).NotEmpty().NotNull().WithMessage("Expression should be not null");
            RuleFor(rl => rl.LocalId).NotEmpty().NotNull().WithMessage("LocalId should be not null");
            RuleFor(rl => rl.Expression).Must(s => s.Contains("WHERE 1=1 ",
                    StringComparison.InvariantCultureIgnoreCase))
                .WithMessage("Query must contain WHERE 1=1");
            RuleFor(rl => rl.Expression).NotAllowedWordsExpressionValidator(new List<string>
            {
                "delete ", " delete", " delete ",
                "username ", " username", " username ",
                "password ", " password", " password ",
                "user ", " user", " user ",
                "update ", " update", " update ",
                "set ", " set", " set ", "\nset ", " set "
            }).When(rl => rl.QualityAction != QualityAction.AUT);
            RuleFor(rl => rl.Expression).NotAllowedWordsExpressionValidator(new List<string>
            {
                "delete ", " delete", " delete ",
                "username ", " username", " username ",
                "password ", " password", " password ",
                "user ", " user", " user ",
            }).When(rl => rl.QualityAction == QualityAction.AUT);
            RuleFor(rl => rl.Expression).Must(s => 
                    s.StartsWith("SELECT b.GlobalID AS buildingId", StringComparison.InvariantCultureIgnoreCase))
                .When(rl => rl.QualityAction != QualityAction.AUT 
                            && rl.EntityType == EntityType.BUILDING)
                .WithMessage("Query must start with \'SELECT b.GlobalID AS buildingId\'");
            RuleFor(rl => rl.Expression).Must(s => 
                    s.StartsWith("SELECT e.GlobalID AS entranceId, e.EntBuildingId AS buildingId", 
                        StringComparison.InvariantCultureIgnoreCase))
                            .When(rl => rl.QualityAction != QualityAction.AUT 
                                        && rl.EntityType == EntityType.ENTRANCE)
                            .WithMessage("Query must start with \'SELECT e.GlobalID AS entranceId, e.EntBuildingId AS buildingId\'");
            RuleFor(rl => rl.Expression).Must(s => 
                    s.StartsWith("SELECT d.GlobalId AS dwellingId, e.GlobalID AS entranceId, e.EntBuildingId AS buildingId", 
                        StringComparison.InvariantCultureIgnoreCase))
                .When(rl => rl.QualityAction != QualityAction.AUT 
                            && rl.EntityType == EntityType.DWELLING)
                .WithMessage("Query must start with \'SELECT d.GlobalId AS dwellingId, e.GlobalID AS entranceId, e.EntBuildingId AS buildingId\'");
            RuleFor(rl => rl.Expression).Must(s => 
                    s.StartsWith("UPDATE [buildingregister].[dataown].[BUILDINGS]", 
                        StringComparison.InvariantCultureIgnoreCase) 
                        || s.StartsWith("WITH", StringComparison.InvariantCultureIgnoreCase))
                .When(rl => rl.QualityAction == QualityAction.AUT && rl.EntityType == EntityType.BUILDING)
                .WithMessage("Query must start with \'UPDATE [buildingregister].[dataown].[BUILDINGS]\'");
            RuleFor(rl => rl.Expression).Must(s => 
                    s.StartsWith("UPDATE [buildingregister].[dataown].[ENTRANCE]", 
                        StringComparison.InvariantCultureIgnoreCase)
                        || s.StartsWith("WITH", StringComparison.InvariantCultureIgnoreCase))
                .When(rl => rl.QualityAction == QualityAction.AUT && rl.EntityType == EntityType.ENTRANCE)
                .WithMessage("Query must start with \'UPDATE [buildingregister].[dataown].[ENTRANCE]\'");
            RuleFor(rl => rl.Expression).Must(s => 
                    s.StartsWith("UPDATE [buildingregister].[dataown].[DWELLING]", 
                        StringComparison.InvariantCultureIgnoreCase)
                        || s.StartsWith("WITH", StringComparison.InvariantCultureIgnoreCase))
                .When(rl => rl.QualityAction == QualityAction.AUT && rl.EntityType == EntityType.ENTRANCE)
                .WithMessage("Query must start with \'UPDATE [buildingregister].[dataown].[DWELLING]\'")
                ;
        }
    }
}
