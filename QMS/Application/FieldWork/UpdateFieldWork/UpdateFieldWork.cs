
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
            fieldwork.Description = request.Description;
            fieldwork.EmailTemplateId = request.EmailTemplateId;
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
