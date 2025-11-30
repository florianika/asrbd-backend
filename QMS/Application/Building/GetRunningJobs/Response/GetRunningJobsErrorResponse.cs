
namespace Application.Building.GetRunningJobs.Response
{
    public class GetRunningJobsErrorResponse : GetRunningJobsResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
