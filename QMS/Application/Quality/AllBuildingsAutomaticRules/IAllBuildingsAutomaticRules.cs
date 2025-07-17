using Application.Quality.AllBuildingsAutomaticRules.Request;
using Application.Quality.AllBuildingsAutomaticRules.Response;

namespace Application.Quality.AllBuildingsAutomaticRules
{
    public interface IAllBuildingsAutomaticRules : IQualityCheck<AllBuildingsAutomaticRulesRequest, AllBuildingsAutomaticRulesResponse>
    {
    }
}
