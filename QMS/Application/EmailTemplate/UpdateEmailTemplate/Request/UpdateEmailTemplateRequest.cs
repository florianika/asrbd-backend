
namespace Application.EmailTemplate.UpdateEmailTemplate.Request
{
    public class UpdateEmailTemplateRequest : EmailTemplate.Request
    {
        public int EmailTemplateId { get; set; }
        //TODO check if this should be declared as required
        public string Subject { get; set; }
        //TODO check if this should be declared as required
        public string Body { get; set; }
        public Guid UpdatedUser { get; set; }
    }
}
