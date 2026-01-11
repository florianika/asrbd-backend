using Application.Queries.GetBuildingWithQuePendingLogs.Request;
using Application.Queries.GetBuildingWithQuePendingLogs.Response;

namespace Application.Queries.GetBuildingWithQuePendingLogs
{
    public interface IGetBuildingWithQuePendingLogs
    {
        Task<GetBuildingWithQuePendingLogsResponse> Execute(GetBuildingWithQuePendingLogsRequest request);
    }
}
