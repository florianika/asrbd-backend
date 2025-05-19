
namespace Application.EmailTemplate.UpdateEmailTemplate.Response
{
    public class UpdateEmailTemplateErrorResponse : UpdateEmailTemplateResponse
    {
        public required string Message { get; set; }
        public required string Code { get; set; }
    }
}
