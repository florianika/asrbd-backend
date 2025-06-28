using Domain.Enum;

namespace Application.FieldWork.GetJobStatus.Response
{
    public class GetJobStatusSuccessResponse : GetJobStatusResponse
    {
        public JobStatus Status { get; set; }
    }
}
