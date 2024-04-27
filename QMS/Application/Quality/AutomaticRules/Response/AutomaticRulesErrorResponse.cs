
namespace Application.Quality.AutomaticRules.Response
{
    public class AutomaticRulesErrorResponse : AutomaticRulesResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
