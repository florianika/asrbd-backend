

namespace Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId.Response
{
    public class GetProcessOutputLogsByDwellingIdSuccessResponse : GetProcessOutputLogsByDwellingIdResponse
    {
        public IEnumerable<ProcessOutputLogDTO> ProcessOutputLogDto { get; set; } = null!;
    }
}
