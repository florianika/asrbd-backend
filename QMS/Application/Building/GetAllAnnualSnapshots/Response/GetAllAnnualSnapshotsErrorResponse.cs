
namespace Application.Building.GetAllAnnualSnapshots.Response
{
    public class GetAllAnnualSnapshotsErrorResponse : GetAllAnnualSnapshotsResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
