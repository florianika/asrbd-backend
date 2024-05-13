using System;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Response
{
    public class GetProcessOutputLogsByBuldingIdAndStatusErrorResponse : GetProcessOutputLogsByBuildingIdAndStatusResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
