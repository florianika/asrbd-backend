
namespace Application.EmailTemplate.GetEmailTemplate.Response
{
    public class GetEmailTemplateSuccessResponse : GetEmailTemplateResponse
    {
        public required EmailTemplateDTO EmailTemplateDTO { get; set; }
    }
}
