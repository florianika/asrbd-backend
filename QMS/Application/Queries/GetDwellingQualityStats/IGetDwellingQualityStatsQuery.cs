using Application.Queries.GetDwellingQualityStats.Response;

namespace Application.Queries.GetDwellingQualityStats
{
    public interface IGetDwellingQualityStatsQuery
    {
        Task<GetDwellingQualityStatsResponse> Execute();
    }
}
