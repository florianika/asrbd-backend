
namespace Application.Rule.GetRulesByVarableAndEntity.Response
{
    public class GetRulesByVarableAndEntitySuccessResponse : GetRulesByVarableAndEntityResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
