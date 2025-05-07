using Application.Common.Translators;
using Application.FieldWork.GetActiveFieldWork.Response;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.Ports;

namespace Application.FieldWork.GetActiveFieldWork
{
    public class GetActiveFieldWork : IGetActiveFieldWork
    {
        private readonly IFieldWorkRepository _filedWorkRepository;

        public GetActiveFieldWork(IFieldWorkRepository filedWorkRepository)
        {
            _filedWorkRepository = filedWorkRepository;
        }
        public async Task<GetActiveFieldWorkResponse> Execute()
        {
            var fieldwork = await _filedWorkRepository.GetActiveFieldWork();
            var fieldworkDto = Translator.ToDTO(fieldwork);
            return new GetActiveFieldWorkSuccessResponse
            {
                FieldWorkDTO = fieldworkDto
            };
        }
    }
}
