
namespace Application.Rule.GetAllRules.Response
{
    public class GetAllRulesErrorResponse : GetAllRulesResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
