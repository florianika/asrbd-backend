
using Application.Common.Translators;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId
{
    public class GetProcessOutputLogsByEntranceId : IGetProcessOutputLogsByEntranceId
    {
        private readonly IProcessOutputLogRepository _processOutputLogRepository;
        public GetProcessOutputLogsByEntranceId(IProcessOutputLogRepository processOutputLogRepository)
        {
            _processOutputLogRepository = processOutputLogRepository;
        }
        public async Task<GetProcessOutputLogsByEntranceIdResponse> Execute(GetProcessOutputLogsByEntranceIdRequest request)
        {
            var processOutputLogs = await _processOutputLogRepository.GetProcessOutputLogsByEntranceId(request.EntId);

            return new GetProcessOutputLogsByEntranceIdSuccessResponse
            {
                ProcessOutputLogDto = Translator.ToDTOList(processOutputLogs)
            };
        }
    }
}
