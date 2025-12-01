using Application.Queries.GetBuildingQualityStats.Response;

namespace Application.Queries.GetBuildingQualityStats
{
    public interface IGetBuildingQualityStatsQuery
    {
        Task<GetBuildingQualityStatsResponse> Execute();
    }
}
