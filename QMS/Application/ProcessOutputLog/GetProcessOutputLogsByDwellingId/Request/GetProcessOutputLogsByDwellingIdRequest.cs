

namespace Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId.Request
{
    public class GetProcessOutputLogsByDwellingIdRequest : ProcessOutputLog.Request
    {
        public Guid DwlId { get; set; }
    }
}
