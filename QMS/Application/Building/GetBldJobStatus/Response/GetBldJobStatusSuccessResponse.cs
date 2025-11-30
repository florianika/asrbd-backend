using Domain.Enum;

namespace Application.Building.GetBldJobStatus.Response
{
    public class GetBldJobStatusSuccessResponse : GetBldJobStatusResponse
    {
        public JobStatus Status { get; set; }
    }
}
