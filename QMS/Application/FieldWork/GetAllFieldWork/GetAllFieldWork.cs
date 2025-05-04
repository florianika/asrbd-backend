
using Application.Common.Translators;
using Application.FieldWork.GetAllFieldWork.Response;
using Application.Ports;
using Application.Rule.GetAllRules.Response;

namespace Application.FieldWork.GetAllFieldWork
{
    public class GetAllFieldWork : IGetAllFieldWork
    {
        private readonly IFiledWorkRepository _filedWorkRepository;

        public GetAllFieldWork(IFiledWorkRepository filedWorkRepository)
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
