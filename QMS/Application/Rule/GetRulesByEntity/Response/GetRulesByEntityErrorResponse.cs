
namespace Application.Rule.GetRulesByEntity.Response
{
    public class GetRulesByEntityErrorResponse : GetRulesByEntityResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
