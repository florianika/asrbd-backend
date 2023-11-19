
namespace Application.Rule.GetRulesByQualityAction.Response
{
    public class GetRulesByQualityActionSuccessResponse : GetRulesByQualityActionResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
