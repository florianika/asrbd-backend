namespace Application.EmailTemplate.CreateEmailTemplate.Response
{
    public class CreateEmailTemplateErrorResponse : CreateEmailTemplateResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
