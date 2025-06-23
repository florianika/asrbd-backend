
namespace Application.FieldWorkRule.GetRuleByFieldWorkAndEntity.Response
{
    public class GetRuleByFieldWorkAndEntitySuccessResponse : GetRuleByFieldWorkAndEntityResponse
    {
        public IEnumerable<FieldWorkRuleDTO> FieldworkRulesDTO { get; set; } = new List<FieldWorkRuleDTO>();
    }
}
