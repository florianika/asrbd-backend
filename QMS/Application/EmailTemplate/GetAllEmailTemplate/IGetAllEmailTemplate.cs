using Application.EmailTemplate.GetAllEmailTemplate.Response;

namespace Application.EmailTemplate.GetAllEmailTemplate
{
    public interface IGetAllEmailTemplate
    {
        Task<GetAllEmailTemplateResponse> Execute();
    }
}
