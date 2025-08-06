
namespace Application.FieldWork.UpdateFieldWork.Request
{
    public class UpdateFieldWorkRequest : FieldWork.Request
    {
        public int FieldWorkId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string FieldWorkName { get; set; }
        public string? Description { get; set; }
        public int OpenEmailTemplateId { get; set; }
        public int CloseEmailTemplateId { get; set; }
        public string? Remarks { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
