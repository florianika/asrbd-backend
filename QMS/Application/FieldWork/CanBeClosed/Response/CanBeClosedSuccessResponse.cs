namespace Application.FieldWork.CanBeClosed.Response
{
    public class CanBeClosedSuccessResponse : CanBeClosedResponse
    {
        public int FieldWorkId { get; set; }
        public bool CanBeClosed { get; set; }
        public string? Reasons { get; set; }
        public DateTime? LastChecked { get; set; }
    }
}
