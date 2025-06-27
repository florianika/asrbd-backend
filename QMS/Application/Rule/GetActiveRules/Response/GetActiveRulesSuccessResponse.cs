namespace Application.Rule.GetActiveRules.Response
{
    public class GetActiveRulesSuccessResponse : GetActiveRulesResponse
    {
        public IEnumerable<ShortRuleDTO> ShortRulesDTO { get; set; } = new List<ShortRuleDTO>();
    }
}
