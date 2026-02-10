namespace Application.Rule.GetRulesByEntityAndStatus.Response
{
    public class GetRulesByEntityAndStatusSuccessResponse : GetRulesByEntityAndStatusResponse
    {
        public required IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
