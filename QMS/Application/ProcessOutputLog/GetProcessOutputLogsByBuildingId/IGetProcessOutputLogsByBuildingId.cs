
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId
{
    public interface IGetProcessOutputLogsByBuildingId : IProcessOutputLog<GetProcessOutputLogsByBuildingIdRequest, GetProcessOutputLogsByBuildingIdResponse>
    {
    }
}
