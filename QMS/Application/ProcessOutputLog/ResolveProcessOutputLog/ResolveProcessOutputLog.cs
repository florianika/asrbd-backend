using Application.Ports;
using Application.ProcessOutputLog.ResolveProcessOutputLog.Request;
using Application.ProcessOutputLog.ResolveProcessOutputLog.Response;
using Microsoft.Extensions.Logging;

namespace Application.ProcessOutputLog.ResolveProcessOutputLog;

public class ResolveProcessOutputLog : IResolveProcessOutputLog
{
    private readonly ILogger _logger;
    private readonly IProcessOutputLogRepository _processOutputLogRepository;

    public ResolveProcessOutputLog(ILogger<ResolveProcessOutputLog> logger, IProcessOutputLogRepository processOutputLogRepository)
    {
        _logger = logger;
        _processOutputLogRepository = processOutputLogRepository;
    }

    public async Task<ResolveProcessOutputLogResponse> Execute(ResolveProcessOutputLogRequest request)
    {
        try
        {
            await _processOutputLogRepository.ResolveProcessOutputLog(request.ProcessOutputLogId, request.UpdatedUser);
            return new ResolveProcessOutputLogSuccessResponse
            {
                ProcessOutputLogId = request.ProcessOutputLogId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while resolving process output log with id: {id}", 
                request.ProcessOutputLogId.ToString());
            throw;
        }
    }
}