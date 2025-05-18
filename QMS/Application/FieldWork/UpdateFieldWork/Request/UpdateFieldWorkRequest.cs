
namespace Application.FieldWork.UpdateFieldWork.Request
{
    public class UpdateFieldWorkRequest : FieldWork.Request
    {
        public int FieldWorkId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public int EmailTemplateId { get; set; }
        public string? Remarks { get; set; }
        public Guid UpdatedUser { get; set; }
    }
    
    //TODO add validator class for request
}
