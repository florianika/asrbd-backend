
using Application.FieldWork.OpenFieldWork.Request;
using Application.FieldWork.OpenFieldWork.Response;
using Application.FieldWork.UpdateFieldWork.Request;
using Application.FieldWork.UpdateFieldWork.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.OpenFieldWork
{
    public class OpenFieldWork : IOpenFieldWork
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public OpenFieldWork(ILogger<OpenFieldWork> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger;
            _fieldWorkRepository = fieldWorkRepository;
        }
        public async Task<OpenFieldWorkResponse> Execute(OpenFieldWorkRequest request)
        {
            var fieldwork = await _fieldWorkRepository.GetFieldWork(request.FieldWorkId);
            if (fieldwork.fieldWorkStatus != Domain.Enum.FieldWorkStatus.NEW)
                return new OpenFieldWorkErrorResponse 
                { 
                    Code = "500", Message = "FieldWork can not be open" 
                };
            fieldwork.fieldWorkStatus = Domain.Enum.FieldWorkStatus.OPEN;
            fieldwork.UpdatedUser = request.UpdatedUser;
            fieldwork.UpdatedTimestamp = DateTime.Now;

            await _fieldWorkRepository.UpdateFieldWork(fieldwork);

            return new OpenFieldWorkSuccessResponse
            {
                Message = "FieldWork status open"
            };
        }
    }
}
