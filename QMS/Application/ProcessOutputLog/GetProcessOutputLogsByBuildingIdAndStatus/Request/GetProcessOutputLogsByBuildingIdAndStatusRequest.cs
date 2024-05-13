using Domain.Enum;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Request
{
    public class GetProcessOutputLogsByBuildingIdAndStatusRequest : ProcessOutputLog.Request
    {
        public Guid BldId { get; set; }
        public QualityStatus QualityStatus {get; set;}
    }
}
