using Application.Common.Translators;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingIdAndStatus
{
    public class GetProcessOutputLogsByBuildingIdAndStatus : IGetProcessOutputLogsByBuildingIdAndStatus
    {

        private readonly IProcessOutputLogRepository _processOutputLogRepository;

        public GetProcessOutputLogsByBuildingIdAndStatus(IProcessOutputLogRepository processOutputLogRepository)
        {
            _processOutputLogRepository = processOutputLogRepository;
        }
        
        public async Task<GetProcessOutputLogsByBuildingIdAndStatusResponse> Execute(GetProcessOutputLogsByBuildingIdAndStatusRequest request)
        {
            var processOutputLogs = await _processOutputLogRepository.GetProcessOutputLogsByBuildingId(request.BldId);

            return new GetProcessOutputLogsByBuildingIdAndStatusSuccessResponse
            {
                ProcessOutputLogDto = Translator.ToDTOList(processOutputLogs)
            };
        }
    }
}
