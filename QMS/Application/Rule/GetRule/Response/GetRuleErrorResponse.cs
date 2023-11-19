
namespace Application.Rule.GetRule.Response
{
    public class GetRuleErrorResponse : GetRuleResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
