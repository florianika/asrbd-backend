
using Application.FieldWork;

namespace Application.FieldWorkRule.GetFieldWorkRule.Response
{
    public class GetFieldWorkRuleSuccessResponse : GetFieldWorkRuleResponse
    {
        //TODO check if this can be declared as required
        public FieldWorkRuleDTO FieldWorkRuleDTO { get; set; }
    }
}
