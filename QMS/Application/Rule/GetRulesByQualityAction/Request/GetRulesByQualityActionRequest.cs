
using Domain.Enum;

namespace Application.Rule.GetRulesByQualityAction.Request
{
    public class GetRulesByQualityActionRequest: Rule.Request
    {
        public QualityAction QualityAction { get; set; }
    }
}
