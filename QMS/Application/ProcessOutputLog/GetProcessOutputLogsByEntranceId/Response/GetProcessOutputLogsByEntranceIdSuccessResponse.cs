
namespace Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Response
{
    public class GetProcessOutputLogsByEntranceIdSuccessResponse : GetProcessOutputLogsByEntranceIdResponse
    {
        public IEnumerable<ProcessOutputLogDTO> ProcessOutputLogDTO { get; set; }
    }
}
