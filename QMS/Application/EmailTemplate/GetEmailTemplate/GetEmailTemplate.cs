
using Application.Common.Translators;
using Application.EmailTemplate.GetEmailTemplate.Request;
using Application.EmailTemplate.GetEmailTemplate.Response;
using Application.Ports;

namespace Application.EmailTemplate.GetEmailTemplate
{
    public class GetEmailTemplate : IGetEmailTemplate
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        public GetEmailTemplate(IEmailTemplateRepository emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }
        public async Task<GetEmailTemplateResponse> Execute(GetEmailTemplateRequest request)
        {
            var emailTemplate = await _emailTemplateRepository.GetEmailTemplate(request.Id);
            var emailTemplateDto = Translator.ToDTO(emailTemplate);
            return new GetEmailTemplateSuccessResponse
            {
                EmailTemplateDTO = emailTemplateDto
            };
        }
    }
}
