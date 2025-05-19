
using Application.FieldWork;

namespace Application.FieldWorkRule.GetFieldWorkRule.Response
{
    public class GetFieldWorkRuleSuccessResponse : GetFieldWorkRuleResponse
    {
        public required FieldWorkRuleDTO FieldWorkRuleDTO { get; set; }
    }
}
