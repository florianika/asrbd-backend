using Application.Queries.GetBuildingSummaryStats.Response;

namespace Application.Queries.GetBuildingSummaryStats
{
    public interface IGetBuildingSummaryStatsQuery
    {
        Task<GetBuildingSummaryStatsResponse> Execute();
    }
}
