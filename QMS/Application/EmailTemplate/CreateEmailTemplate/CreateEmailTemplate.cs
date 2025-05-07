
using Application.EmailTemplate.CreateEmailTemplate.Request;
using Application.EmailTemplate.CreateEmailTemplate.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.EmailTemplate.CreateEmailTemplate
{
    public class CreateEmailTemplate : ICreateEmailTemplate
    {
        private readonly ILogger _logger;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        public CreateEmailTemplate(ILogger<CreateEmailTemplate> logger, IEmailTemplateRepository emailTemplateRepository)
        {
            _logger = logger;
            _emailTemplateRepository = emailTemplateRepository;
        }
        public async Task<CreateEmailTemplateResponse> Execute(CreateEmailTemplateRequest request)
        {
            try
            {
                var emailTemplate = new Domain.EmailTemplate
                {
                    Subject = request.Subject,
                    Body = request.Body,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                    UpdatedUser = null,
                    UpdatedTimestamp = null
                };
                var result = await _emailTemplateRepository.CreateEmailTemplate(emailTemplate);
                return new CreateEmailTemplateSuccessResponse
                {
                    EmailTemplateId = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
