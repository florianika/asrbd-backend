
using Application.Exceptions;
using Application.FieldWork.OpenFieldWork.Request;
using Application.FieldWork.OpenFieldWork.Response;
using Application.FieldWork.UpdateFieldWork.Request;
using Application.FieldWork.UpdateFieldWork.Response;
using Application.Ports;
using Domain.Enum;
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
            var fieldwork = await _fieldWorkRepository.GetFieldWorkByIdAndStatus(request.FieldWorkId, FieldWorkStatus.NEW);
            fieldwork.FieldWorkStatus = Domain.Enum.FieldWorkStatus.OPEN;
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
