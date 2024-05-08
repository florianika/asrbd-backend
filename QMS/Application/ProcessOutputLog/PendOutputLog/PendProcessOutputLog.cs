using System;
using Application.Ports;
using Application.ProcessOutputLog.PendOutputLog.Request;
using Application.ProcessOutputLog.PendOutputLog.Response;
using Microsoft.Extensions.Logging;

namespace Application.ProcessOutputLog.PendOutputLog
{
    public class PendProcessOutputLog : IPendProcessOutputLog
    {
        private readonly ILogger _logger;
        private readonly IProcessOutputLogRepository _processOutputLogRepository;

        public PendProcessOutputLog(ILogger<PendProcessOutputLog> logger, IProcessOutputLogRepository processOutputLogRepository) 
        {
            _logger = logger;
            _processOutputLogRepository = processOutputLogRepository;
        }
        public async Task<PendProcessOutputLogResponse> Execute(PendProcessOutputLogRequest request)
        {
            try
        {
            await _processOutputLogRepository.PendProcessOutputLog(request.ProcessOutputLogId, request.UpdatedUser);
            return new PendProcessOutputLogSuccessResponse
            {
                ProcessOutputLogId = request.ProcessOutputLogId
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while pending process output log with id: {id}", 
            request.ProcessOutputLogId.ToString());
            throw;
        }
        }
    }
}
