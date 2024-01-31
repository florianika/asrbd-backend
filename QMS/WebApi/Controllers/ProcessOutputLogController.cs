using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/outputlogs")]
    public class ProcessOutputLogController : ControllerBase
    {
        private readonly GetProcessOutputLogsByBuildingId _getProcessOutputLogsByBuildingId;
        private readonly GetProcessOutputLogsByEntranceId _getProcessOutputLogsByEntranceId;
        public ProcessOutputLogController(GetProcessOutputLogsByBuildingId getProcessOutputLogsByBuildingId,
            GetProcessOutputLogsByEntranceId getProcessOutputLogsByEntranceId) 
        { 
            _getProcessOutputLogsByBuildingId = getProcessOutputLogsByBuildingId;
            _getProcessOutputLogsByEntranceId = getProcessOutputLogsByEntranceId;
        }


        [HttpGet]
        [Route("buildings/{id}")]
        public async Task<GetProcessOutputLogsByBuildingIdResponse> GetProcessOutputLogsByBuildingId(Guid id)
        {
            return await _getProcessOutputLogsByBuildingId.Execute(new GetProcessOutputLogsByBuildingIdRequest() { BldId = id });
        }


        [HttpGet]
        [Route("entrances/{id}")]
        public async Task<GetProcessOutputLogsByEntranceIdResponse> GetProcessOutputLogsByEntranceId(Guid id)
        {
            return await _getProcessOutputLogsByEntranceId.Execute(new GetProcessOutputLogsByEntranceIdRequest() { EntId = id });
        }
    }
}
