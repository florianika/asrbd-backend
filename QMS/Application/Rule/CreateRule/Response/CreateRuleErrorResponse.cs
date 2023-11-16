namespace Application.Rule.CreateRule.Response
{
    public class CreateRuleErrorResponse : CreateRuleResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
