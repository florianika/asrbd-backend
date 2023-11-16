
namespace Application.Rule.GetAllRules.Response
{
    public class GetAllRulesSuccessResponse : GetAllRulesResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; } = new List<RuleDTO>();
    }
}
