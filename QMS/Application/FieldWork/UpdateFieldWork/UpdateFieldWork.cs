
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
