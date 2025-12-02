namespace Application.Building.GetAnnualSnapshotById.Response
{
    public class GetAnnualSnapshotByIdErrorResponse : GetAnnualSnapshotByIdResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
