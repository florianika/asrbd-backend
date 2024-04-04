
using Application.Common.Translators;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId
{
    public class GetProcessOutputLogsByDwellingId : IGetProcessOutputLogsByDwellingId
    {
        private readonly IProcessOutputLogRepository _processOutputLogRepository;
        public GetProcessOutputLogsByDwellingId(IProcessOutputLogRepository processOutputLogRepository)
        {
            _processOutputLogRepository = processOutputLogRepository;
        }
        public async Task<GetProcessOutputLogsByDwellingIdResponse> Execute(GetProcessOutputLogsByDwellingIdRequest request)
        {
            var processOutputLogs = await _processOutputLogRepository.GetProcessOutputLogsByDwellingId(request.DwlId);

            return new GetProcessOutputLogsByDwellingIdSuccessResponse
            {
                ProcessOutputLogDto = Translator.ToDTOList(processOutputLogs)
            };
        }
    }
}
