
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId
{
    public interface IGetProcessOutputLogsByEntranceId : IProcessOutputLog<GetProcessOutputLogsByEntranceIdRequest, GetProcessOutputLogsByEntranceIdResponse>
    {
    }
}
