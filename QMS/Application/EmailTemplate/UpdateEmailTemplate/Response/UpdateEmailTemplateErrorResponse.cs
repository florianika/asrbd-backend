
namespace Application.EmailTemplate.UpdateEmailTemplate.Response
{
    public class UpdateEmailTemplateErrorResponse : UpdateEmailTemplateResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
