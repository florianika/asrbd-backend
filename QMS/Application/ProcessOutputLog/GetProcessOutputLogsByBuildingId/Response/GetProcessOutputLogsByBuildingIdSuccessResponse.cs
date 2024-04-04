
namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response
{
    public class GetProcessOutputLogsByBuildingIdSuccessResponse : GetProcessOutputLogsByBuildingIdResponse
    {
        public IEnumerable<ProcessOutputLogDTO> ProcessOutputLogDto { get; set; } = null!;
    }
}
