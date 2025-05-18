
namespace Application.EmailTemplate.GetEmailTemplate.Response
{
    public class GetEmailTemplateSuccessResponse : GetEmailTemplateResponse
    {
        //TODO check if this should be made required
        public EmailTemplateDTO EmailTemplateDTO { get; set; }
    }
}
