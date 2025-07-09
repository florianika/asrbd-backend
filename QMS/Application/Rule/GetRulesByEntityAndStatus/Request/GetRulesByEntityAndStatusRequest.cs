using Domain.Enum;

namespace Application.Rule.GetRulesByEntityAndStatus.Request
{
    public class GetRulesByEntityAndStatusRequest : Rule.Request
    {
        public EntityType EntityType { get; set; }
        public RuleStatus RuleStatus { get; set; }
    }
}
