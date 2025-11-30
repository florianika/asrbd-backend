namespace Application.Building.GetRunningJobs.Response
{
    public class GetRunningJobsSuccessResponse : GetRunningJobsResponse
    {
        public IEnumerable<JobDTO> JobsDTO { get; set; } = new List<JobDTO>();
    }
}
