namespace Application.Building.GetAllAnnualSnapshots.Response
{
    public class GetAllAnnualSnapshotsSuccessResponse : GetAllAnnualSnapshotsResponse
    {
        public IEnumerable<DownloadJobDTO> DownloadJobsDTO { get; set; } = new List<DownloadJobDTO>();
    }
}
