using System;
using Application.ProcessOutputLog.PendOutputLog.Request;
using Application.ProcessOutputLog.PendOutputLog.Response;

namespace Application.ProcessOutputLog.PendOutputLog
{
    public interface IPendProcessOutputLog : IProcessOutputLog<PendProcessOutputLogRequest, PendProcessOutputLogResponse>
    {
        
    }
}
