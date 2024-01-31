using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
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
        public ProcessOutputLogController(GetProcessOutputLogsByBuildingId getProcessOutputLogsByBuildingId) 
        { 
            _getProcessOutputLogsByBuildingId = getProcessOutputLogsByBuildingId;
        }


        [HttpGet]
        [Route("buildings/{id}")]
        public async Task<GetProcessOutputLogsByBuildingIdResponse> GetProcessOutputLogsByBuildingId(Guid id)
        {
            return await _getProcessOutputLogsByBuildingId.Execute(new GetProcessOutputLogsByBuildingIdRequest() { BldId = id });
        }
    }
}
