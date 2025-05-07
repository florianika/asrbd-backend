using Application.EmailTemplate.CreateEmailTemplate.Request;
using Application.EmailTemplate.CreateEmailTemplate.Response;

namespace Application.EmailTemplate.CreateEmailTemplate
{
    public interface ICreateEmailTemplate : IEmailTemplate<CreateEmailTemplateRequest, CreateEmailTemplateResponse>
    {
    }
}
