
using Application.Quality.AutomaticRules.Response;
using Application.Quality.AutomaticRules.Request;

namespace Application.Quality.AutomaticRules
{
    public interface IAutomaticRules : IQualityCheck<AutomaticRulesRequest, AutomaticRulesResponse>
    {
    }
}
