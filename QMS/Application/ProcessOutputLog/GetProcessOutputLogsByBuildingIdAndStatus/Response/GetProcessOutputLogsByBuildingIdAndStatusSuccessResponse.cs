using System;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Response
{
    public class GetProcessOutputLogsByBuildingIdAndStatusSuccessResponse : GetProcessOutputLogsByBuildingIdAndStatusResponse
    {
        public IEnumerable<ProcessOutputLogDTO> ProcessOutputLogDto { get; set; } = null!;
    }
}
