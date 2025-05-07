
using Application.EmailTemplate.UpdateEmailTemplate.Request;
using Application.EmailTemplate.UpdateEmailTemplate.Response;
using Application.Ports;
using Application.Rule.UpdateRule.Response;
using Microsoft.Extensions.Logging;

namespace Application.EmailTemplate.UpdateEmailTemplate
{
    public class UpdateEmailTemplate : IUpdateEmailTemplate
    {
        private readonly ILogger _logger;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        public UpdateEmailTemplate(ILogger<UpdateEmailTemplate> logger, IEmailTemplateRepository EmailTemplateRepository)
        {
            _logger = logger;
            _EmailTemplateRepository = EmailTemplateRepository;
        }
        public async Task<UpdateEmailTemplateResponse> Execute(UpdateEmailTemplateRequest request)
        {
            var EmailTemplate = await _EmailTemplateRepository.GetEmailTemplate(request.EmailTemplateId);
            EmailTemplate.Subject = request.Subject;
            EmailTemplate.Body = request.Body;
            EmailTemplate.UpdatedUser = request.UpdatedUser;
            EmailTemplate.UpdatedTimestamp = DateTime.Now;

            await _EmailTemplateRepository.UpdateEmailTemplate(EmailTemplate);

            return new UpdateEmailTemplateSuccessResponse
            {
                Message = "EmailTemplate updated"
            };
        }
    }
}
