

namespace Application.Rule.GetRulesByVariableAndEntity.Response
{
    public class GetRulesByVariableAndEntityErrorResponse : GetRulesByVariableAndEntityResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
