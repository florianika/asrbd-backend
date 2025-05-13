using Application.Queries.GetStatisticsFromBuilding.Response;

namespace Application.Queries.GetStatisticsFromBuilding
{
    public interface IGetStatisticsFromBuildingQuery
    {
        Task<GetStatisticsFromBuildingResponse> Execute();
    }
}
