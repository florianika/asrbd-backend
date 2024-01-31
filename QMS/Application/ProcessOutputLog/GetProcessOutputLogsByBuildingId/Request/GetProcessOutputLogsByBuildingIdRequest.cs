

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request
{
    public class GetProcessOutputLogsByBuildingIdRequest : ProcessOutputLog.Request
    {
        public Guid BldId { get; set; }
    }
}
