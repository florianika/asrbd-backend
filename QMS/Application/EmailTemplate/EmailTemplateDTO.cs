namespace Application.EmailTemplate
{
    public class EmailTemplateDTO
    {
        public int EmailTemplateId { get; set; }
        //TODO check if this should be declared as required
        public string Subject { get; set; }
        //TODO check if this should be declared as required
        public string Body { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
    }
}
