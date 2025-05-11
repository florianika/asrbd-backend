
using Application.FieldWork;

namespace Application.FieldWorkRule.GetRuleByFieldWork.Response
{
    public class GetRuleByFieldWorkSuccessResponse : GetRuleByFieldWorkResponse
    {
        public IEnumerable<FieldWorkRuleDTO> FieldworkRulesDTO { get; set; } = new List<FieldWorkRuleDTO>();
    }
}
