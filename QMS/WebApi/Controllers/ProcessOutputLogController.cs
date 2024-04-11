using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByDwellingId.Response;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByEntranceId.Response;
using Application.ProcessOutputLog.ResolveProcessOutputLog;
using Application.ProcessOutputLog.ResolveProcessOutputLog.Request;
using Application.ProcessOutputLog.ResolveProcessOutputLog.Response;
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
        private readonly GetProcessOutputLogsByDwellingId _getProcessOutputLogsByDwellingId;
        private readonly IResolveProcessOutputLog _resolveProcessOutputLogService;
        private readonly IAuthTokenService _authTokenService;
        public ProcessOutputLogController(GetProcessOutputLogsByBuildingId getProcessOutputLogsByBuildingId,
            GetProcessOutputLogsByEntranceId getProcessOutputLogsByEntranceId,
            GetProcessOutputLogsByDwellingId getProcessOutputLogsByDwellingId, 
            IResolveProcessOutputLog resolveProcessOutputLog, IAuthTokenService authTokenService) 
        { 
            _getProcessOutputLogsByBuildingId = getProcessOutputLogsByBuildingId;
            _getProcessOutputLogsByEntranceId = getProcessOutputLogsByEntranceId;
            _getProcessOutputLogsByDwellingId = getProcessOutputLogsByDwellingId;
            _resolveProcessOutputLogService = resolveProcessOutputLog;
            _authTokenService = authTokenService;
        }

        [HttpPatch]
        [Route("resolve/{id:guid}")]
        public async Task<ResolveProcessOutputLogResponse> ResolveProcessOutputLog(Guid id)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            var updatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _resolveProcessOutputLogService.Execute(new ResolveProcessOutputLogRequest() 
                { ProcessOutputLogId = id, UpdatedUser = updatedUser});
        }

        [HttpGet]
        [Route("buildings/{id:guid}")]
        public async Task<GetProcessOutputLogsByBuildingIdResponse> GetProcessOutputLogsByBuildingId(Guid id)
        {
            return await _getProcessOutputLogsByBuildingId.Execute(new GetProcessOutputLogsByBuildingIdRequest() { BldId = id });
        }


        [HttpGet]
        [Route("entrances/{id:guid}")]
        public async Task<GetProcessOutputLogsByEntranceIdResponse> GetProcessOutputLogsByEntranceId(Guid id)
        {
            return await _getProcessOutputLogsByEntranceId.Execute(new GetProcessOutputLogsByEntranceIdRequest() { EntId = id });
        }

        [HttpGet]
        [Route("dwellings/{id:guid}")]
        public async Task<GetProcessOutputLogsByDwellingIdResponse> GetProcessOutputLogsByDwellingId(Guid id)
        {
            return await _getProcessOutputLogsByDwellingId.Execute(new GetProcessOutputLogsByDwellingIdRequest() { DwlId = id });
        }
    }
}
