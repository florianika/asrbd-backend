
namespace Application.Rule.GetRulesByEntity.Response
{
    public class GetRulesByEntitySuccessResponse : GetRulesByEntityResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
