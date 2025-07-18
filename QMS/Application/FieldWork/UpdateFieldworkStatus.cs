
using Application.Ports;
using Domain.Enum;

namespace Application.FieldWork
{
    public class UpdateFieldworkStatus
    {
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public UpdateFieldworkStatus(IFieldWorkRepository fieldWorkRepository)
        {
            _fieldWorkRepository = fieldWorkRepository;
        }
        public async Task<bool> ExecuteAsync(int fieldworkId, FieldWorkStatus status)
        {
            var fieldwork = await _fieldWorkRepository.GetFieldWork(fieldworkId);
            if (fieldwork == null)
                throw new Exception("Fieldwork not found.");

            fieldwork.FieldWorkStatus = status;
            await _fieldWorkRepository.UpdateFieldWork(fieldwork);
            return true;
        }
    }
}
