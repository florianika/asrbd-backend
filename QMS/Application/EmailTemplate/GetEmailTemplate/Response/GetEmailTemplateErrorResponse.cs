namespace Application.EmailTemplate.GetEmailTemplate.Response
{
    public class GetEmailTemplateErrorResponse : GetEmailTemplateResponse
    {
        public string? Message { get; set; }
        public string? Code { get; set; }
    }
}
