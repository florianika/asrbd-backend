namespace Application.Rule.GetRulesByEntityAndStatus.Response
{
    public class GetRulesByEntityAndStatusErrorResponse : GetRulesByEntityAndStatusResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
