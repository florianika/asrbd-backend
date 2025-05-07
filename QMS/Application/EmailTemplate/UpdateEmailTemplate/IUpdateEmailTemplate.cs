using Application.EmailTemplate.UpdateEmailTemplate.Request;
using Application.EmailTemplate.UpdateEmailTemplate.Response;

namespace Application.EmailTemplate.UpdateEmailTemplate
{
    public interface IUpdateEmailTemplate : IEmailTemplate<UpdateEmailTemplateRequest, UpdateEmailTemplateResponse>
    {
    }
}
