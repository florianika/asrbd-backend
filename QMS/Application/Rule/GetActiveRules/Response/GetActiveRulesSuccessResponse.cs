namespace Application.Rule.GetActiveRules.Response
{
    public class GetActiveRulesSuccessResponse : GetActiveRulesResponse
    {
        public IEnumerable<ActiveRuleDTO> ActiveRulesDTO { get; set; } = new List<ActiveRuleDTO>();
    }
}
