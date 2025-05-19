
namespace Application.EmailTemplate.UpdateEmailTemplate.Request
{
    public class UpdateEmailTemplateRequest : EmailTemplate.Request
    {
        public int EmailTemplateId { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
