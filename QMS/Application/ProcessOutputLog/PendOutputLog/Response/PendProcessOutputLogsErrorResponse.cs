using System;

namespace Application.ProcessOutputLog.PendOutputLog.Response
{
    public class PendProcessOutputLogsErrorResponse : PendProcessOutputLogResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
