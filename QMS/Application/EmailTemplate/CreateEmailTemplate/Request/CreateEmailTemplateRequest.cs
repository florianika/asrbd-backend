
namespace Application.EmailTemplate.CreateEmailTemplate.Request
{
    public class CreateEmailTemplateRequest : EmailTemplate.Request
    {
        //TODO check if declaring this required is a good idea to get rid of the warnings
        //TODO example: public required string Subject { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Guid CreatedUser { get; set; }
    }
}
