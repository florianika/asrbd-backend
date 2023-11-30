
namespace Application.Rule.GetRulesByQualityAction.Response
{
    #nullable disable
    public class GetRulesByQualityActionSuccessResponse : GetRulesByQualityActionResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
