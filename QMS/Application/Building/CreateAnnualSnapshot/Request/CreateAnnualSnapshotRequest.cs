namespace Application.Building.CreateAnnualSnapshot.Request
{
    public class CreateAnnualSnapshotRequest : Building.Request
    {
        public int ReferenceYear { get; set; }
        public Guid CreatedBy { get; set; }
        public string? Remarks { get; set; }
    }
}
