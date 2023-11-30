
using Domain.Enum;

namespace Application.Rule.UpdateRule.Request
{
    public class UpdateRuleRequest : Rule.Request
    {
        public long Id { get; set; }
        public string? LocalId { get; set; }
        public EntityType EntityType { get; set; }
        public string? Variable { get; set; }
        public string? NameAl { get; set; }
        public string? NameEn { get; set; }
        public string? DescriptionAl { get; set; }
        public string? DescriptionEn { get; set; }
        public string? Version { get; set; } = "1.0";
        public string? VersionRationale { get; set; } = "First Version";
        public string? Expression { get; set; }
        public QualityAction QualityAction { get; set; }
        public string? RuleRequirement { get; set; }
        public string? Remark { get; set; }
        public string? QualityMessageAl { get; set; }
        public string? QualityMessageEn { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
