
namespace Application.Rule.ChangeRuleStatus.Response
{
    public class ChangeRuleStatusErrorResponse : ChangeRuleStatusResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
