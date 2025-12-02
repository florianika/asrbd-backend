namespace Application.Building.CreateAnnualSnapshot.Response
{
    public class CreateAnnualSnapshotErrorResponse : CreateAnnualSnapshotResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
