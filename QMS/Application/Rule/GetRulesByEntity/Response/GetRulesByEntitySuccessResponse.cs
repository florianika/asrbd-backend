
namespace Application.Rule.GetRulesByEntity.Response
{
    #nullable disable
    public class GetRulesByEntitySuccessResponse : GetRulesByEntityResponse
    {
        public IEnumerable<RuleDTO> RulesDTO { get; set; }
    }
}
