using Application.Queries.GetStatisticsFromRules.Response;

namespace Application.Queries.GetStatisticsFromRules
{
    public interface IGetStatisticsFromRulesQuery
    {
        Task<GetStatisticsFromRulesResponse> Execute();
    }
}
