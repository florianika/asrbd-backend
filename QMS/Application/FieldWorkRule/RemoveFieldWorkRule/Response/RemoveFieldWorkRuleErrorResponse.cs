namespace Application.FieldWorkRule.RemoveFieldWorkRule.Response
{
    public class RemoveFieldWorkRuleErrorResponse : RemoveFieldWorkRuleResponse
    {
        public required string Message { get; set; }
        public required string Code { get; set; }
    }
}
