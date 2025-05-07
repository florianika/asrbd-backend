
using Application.Common.Translators;
using Application.EmailTemplate.GetAllEmailTemplate.Response;
using Application.Ports;

namespace Application.EmailTemplate.GetAllEmailTemplates
{
    public class GetAllEmailTemplate : IGetAllEmailTemplate
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        public GetAllEmailTemplate(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }
        public async Task<GetAllEmailTemplateResponse> Execute()
        {
            var emailTemplates = await _emailTemplateRepository.GetAllEmailTemplates();
            return new GetAllEmailTemplateSuccessResponse
            {
                EmailTemplateDTOs = Translator.ToDTOList(emailTemplates)
            };
        }
    }
}
