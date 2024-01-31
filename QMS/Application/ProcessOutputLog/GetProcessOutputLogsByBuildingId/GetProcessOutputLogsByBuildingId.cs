

using Application.Common.Translators;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;

namespace Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId
{
    public class GetProcessOutputLogsByBuildingId : IGetProcessOutputLogsByBuildingId
    {
        private readonly IProcessOutputLogRepository _processOutputLogRepository;
        public GetProcessOutputLogsByBuildingId(IProcessOutputLogRepository processOutputLogRepository)
        {
            _processOutputLogRepository = processOutputLogRepository;
        }
        public async Task<GetProcessOutputLogsByBuildingIdResponse> Execute(GetProcessOutputLogsByBuildingIdRequest request)
        {
            var processOutputLogs = await _processOutputLogRepository.GetProcessOutputLogsByBuildingId(request.BldId);

            return new GetProcessOutputLogsByBuildingIdSuccessResponse
            {
                ProcessOutputLogDTO = Translator.ToDTOList(processOutputLogs)
            };
        }
    }
}
