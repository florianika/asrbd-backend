
namespace Application.Rule.GetRulesByVariableAndEntity.Response
{
    public class GetRulesByVariableAndEntitySuccessResponse : GetRulesByVariableAndEntityResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
