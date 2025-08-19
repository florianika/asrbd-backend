
using Application.Exceptions;
using Application.FieldWork.UpdateFieldWork.Request;
using Application.FieldWork.UpdateFieldWork.Response;
using Application.Ports;
using Application.Rule.UpdateRule.Response;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.UpdateFieldWork
{
    public class UpdateFieldWork : IUpdateFieldWork
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public UpdateFieldWork(ILogger<UpdateFieldWork> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
        }
        public async Task<UpdateFieldWorkResponse> Execute(UpdateFieldWorkRequest request)
        {
            var fieldwork = await _fieldWorkRepository.GetFieldWork(request.FieldWorkId);
            if(fieldwork.FieldWorkStatus != Domain.Enum.FieldWorkStatus.NEW)
            {
                _logger.LogError("Fieldwork with ID {FieldWorkId} is not open for update.", request.FieldWorkId);
                throw new AppException("Fieldwork is not with status NEW, therefore cannot be updated");
            }
            fieldwork.StartDate = request.StartDate;
            fieldwork.EndDate = request.EndDate;
            fieldwork.FieldWorkName = request.FieldWorkName;
            fieldwork.Description = request.Description;
            fieldwork.OpenEmailTemplateId = request.OpenEmailTemplateId;
            fieldwork.CloseEmailTemplateId = request.CloseEmailTemplateId;
            fieldwork.UpdatedUser = request.UpdatedUser;
            fieldwork.UpdatedTimestamp = DateTime.Now;
            fieldwork.Remarks = request.Remarks;

            await _fieldWorkRepository.UpdateFieldWork(fieldwork);

            return new UpdateFieldWorkSuccessResponse
            {
                Message = "FieldWork updated"
            };
        }
    }
}
