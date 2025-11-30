using Application.Building.GetBldJobStatus.Request;
using Application.Building.GetBldJobStatus.Response;

namespace Application.Building.GetBldJobStatus
{
    public interface IGetBldJobStatus : IBuilding<GetBldJobStatusRequest,GetBldJobStatusResponse>
    {
    }
}
