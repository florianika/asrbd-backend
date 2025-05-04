
using Application.Common.Translators;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.Ports;

namespace Application.FieldWork.GetAllFieldWork
{
    public class GetAllFieldWork : IGetAllFieldWork
    {
        private readonly IFieldWorkRepository _filedWorkRepository;

        public GetAllFieldWork(IFieldWorkRepository filedWorkRepository)
        {
            _filedWorkRepository = filedWorkRepository;
        }
        public async Task<GetAllFieldWorkResponse> Execute()
        {
            var fieldwork = await _filedWorkRepository.GetAllFieldWork();
            return new GetAllFieldWorkSuccessResponse
            {
                FieldworksDTO = Translator.ToDTOList(fieldwork)
            };
        }
    }
}
