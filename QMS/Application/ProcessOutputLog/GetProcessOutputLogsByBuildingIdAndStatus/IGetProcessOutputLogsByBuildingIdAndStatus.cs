using System;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus
{
    public interface IGetProcessOutputLogsByBuildingIdAndStatus : IProcessOutputLog<GetProcessOutputLogsByBuildingIdAndStatusRequest, GetProcessOutputLogsByBuildingIdAndStatusResponse>
    {
        
    }
}
