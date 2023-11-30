
namespace Application.Rule.GetRulesByVariableAndEntity.Response
{
    #nullable disable
    public class GetRulesByVariableAndEntitySuccessResponse : GetRulesByVariableAndEntityResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
