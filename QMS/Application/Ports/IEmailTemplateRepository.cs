
namespace Application.Ports
{
    public interface IEmailTemplateRepository
    {
        Task<int> CreateEmailTemplate(Domain.EmailTemplate emailTemplate);
        Task<List<Domain.EmailTemplate>> GetAllEmailTemplates();
        Task<Domain.EmailTemplate> GetEmailTemplate(int id);
        Task UpdateEmailTemplate(Domain.EmailTemplate emailTemplate);
    }
}
