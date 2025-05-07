
namespace Application.EmailTemplate.CreateEmailTemplate.Request
{
    public class CreateEmailTemplateRequest : EmailTemplate.Request
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
