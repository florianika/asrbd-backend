using Application.FieldWork.AssociateEmailTemplateWithFieldWork.Request;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork.Response;
using Application.FieldWork.UpdateFieldWork.Request;
using Application.FieldWork.UpdateFieldWork.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.AssociateEmailTemplateWithFieldWork
{
    public class AssociateEmailTemplateWithFieldWork : IAssociateEmailTemplateWithFieldWork
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        private readonly IEmailTemplateRepository _emailTemplateRepository;

        public AssociateEmailTemplateWithFieldWork(ILogger<AssociateEmailTemplateWithFieldWork> logger, IFieldWorkRepository fieldWorkRepository, IEmailTemplateRepository emailTemplateRepository)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
            _emailTemplateRepository = emailTemplateRepository;
        }
        public async Task<AssociateEmailTemplateWithFieldWorkResponse> Execute(AssociateEmailTemplateWithFieldWorkRequest request)
        {
            var fieldwork = await _fieldWorkRepository.GetFieldWork(request.FieldWorkId);
            var template = await _emailTemplateRepository.GetEmailTemplate(request.EmailTemplateId);

            if ((request.isOpen))
            {
                 fieldwork.OpenEmailTemplateId = request.EmailTemplateId;
            }
            else
            {
                fieldwork.CloseEmailTemplateId = request.EmailTemplateId;
            }

            fieldwork.UpdatedUser = request.UpdatedUser;
            fieldwork.UpdatedTimestamp = DateTime.Now;

            await _fieldWorkRepository.UpdateFieldWork(fieldwork);

            return new AssociateEmailTemplateWithFieldWorkSuccessResponse
            {
                Message = "email template was associated to fieldwork"
            };
        }
    }
}
