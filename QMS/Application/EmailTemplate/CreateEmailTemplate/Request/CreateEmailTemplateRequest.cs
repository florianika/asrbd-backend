
namespace Application.EmailTemplate.CreateEmailTemplate.Request
{
    public class CreateEmailTemplateRequest : EmailTemplate.Request
    {
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
