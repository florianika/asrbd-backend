namespace Application.Rule.GetRulesByEntityAndStatus.Response
{
    public class GetRulesByEntityAndStatusSuccessResponse : GetRulesByEntityAndStatusResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
