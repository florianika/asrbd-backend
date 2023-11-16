
using Application.Rule;
using Domain.Enum;
using System.Linq.Expressions;

namespace Application.Common.Translators
{
    public static class Translator
    {
        public static List<RuleDTO> ToDTOList(List<Domain.Rule> rule)
        {
            List<RuleDTO> rules = new();
            rule.ForEach((rule) => {
                rules.Add(ToDTO(rule));
            });
            return rules;
        }

        public static RuleDTO ToDTO(Domain.Rule rule)
        {
            return new RuleDTO()
            {
                Id = rule.Id,
                LocalId = rule.LocalId,
                EntityType = rule.EntityType,
                Variable = rule.Variable,
                NameAl = rule.NameAl,
                NameEn = rule.NameEn,
                DescriptionAl = rule.DescriptionAl,
                DescriptionEn = rule.DescriptionEn,
                Version = rule.Version,
                VersionRationale = rule.VersionRationale,
                Expression = rule.Expression,
                QualityAction = rule.QualityAction,
                RuleStatus = rule.RuleStatus,
                RuleRequirement = rule.RuleRequirement,
                Remark = rule.Remark,
                QualityMessageAl = rule.QualityMessageAl,
                QualityMessageEn = rule.QualityMessageEn,
                CreatedUser = rule.CreatedUser,
                CreatedTimestamp = rule.CreatedTimestamp,
                UpdatedUser = rule.UpdatedUser,
                UpdatedTimestamp = rule.UpdatedTimestamp
            };
        }
    }
}
