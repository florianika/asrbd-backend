
namespace Domain
{
    public class EmailTemplate
    {
        public int EmailTemplateId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
    }
}
