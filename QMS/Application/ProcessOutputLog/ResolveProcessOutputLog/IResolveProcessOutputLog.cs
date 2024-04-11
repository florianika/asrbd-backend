using Application.ProcessOutputLog.ResolveProcessOutputLog.Request;
using Application.ProcessOutputLog.ResolveProcessOutputLog.Response;

namespace Application.ProcessOutputLog.ResolveProcessOutputLog;

public interface IResolveProcessOutputLog : IProcessOutputLog<ResolveProcessOutputLogRequest, 
    ResolveProcessOutputLogResponse>
{
    
}