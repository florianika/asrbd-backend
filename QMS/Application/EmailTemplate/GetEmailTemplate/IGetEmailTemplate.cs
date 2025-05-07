using Application.EmailTemplate.GetEmailTemplate.Request;
using Application.EmailTemplate.GetEmailTemplate.Response;

namespace Application.EmailTemplate.GetEmailTemplate
{
    public interface IGetEmailTemplate : IEmailTemplate<GetEmailTemplateRequest, GetEmailTemplateResponse>
    {
    }
}
