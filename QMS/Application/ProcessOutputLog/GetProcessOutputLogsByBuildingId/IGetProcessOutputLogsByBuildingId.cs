
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
using Domain;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId
{
    public interface IGetProcessOutputLogsByBuildingId : IProcessOutputLog<GetProcessOutputLogsByBuildingIdRequest, GetProcessOutputLogsByBuildingIdResponse>
    {
    }
}
