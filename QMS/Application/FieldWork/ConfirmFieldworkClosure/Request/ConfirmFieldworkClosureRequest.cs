namespace Application.FieldWork.ConfirmFieldworkClosure.Request
{
    public class ConfirmFieldworkClosureRequest : FieldWork.Request
    {
        public int FieldWorkId { get; set; }
        public Guid UpdatedUser { get; set; }
        public string? Remarks { get; set; }
    }
}
