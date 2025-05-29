using Application.FieldWorkRule.GetStatisticsByRule.Request;
using Application.Queries.GetStatisticsFromRules.Request;
using Application.Queries.GetStatisticsFromRules.Response;

namespace Application.Queries.GetStatisticsFromRules
{
    public interface IGetStatisticsFromRulesQuery
    {
        Task<GetStatisticsFromRulesResponse> Execute(GetStatisticsFromRulesRequest request);
    }
}
