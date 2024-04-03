using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Ports;
using Application.Quality.BuildingQualityCheck.Response;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality;
using Application.Quality.BuildingQualityCheck;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/check")]
    public class BuildingQualityCheckController : ControllerBase
    {
        private readonly IAuthTokenService _authTokenService;
        private readonly BuildingQualityCheck _buildingQualityCheckService;

        public BuildingQualityCheckController(IAuthTokenService authTokenService, BuildingQualityCheck buildingQualityCheckService) {
            _authTokenService = authTokenService;
            _buildingQualityCheckService = buildingQualityCheckService;
        }
        
        [HttpPost("executeRules")]
        public async Task<BuildingQualityCheckResponse> BuildingQualityCheck(BuildingQualityCheckRequest request) {            
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            var executionUser = await _authTokenService.GetUserIdFromToken(token);
            request.ExecutionUser = executionUser;
            return await _buildingQualityCheckService.Execute(request, "ExecuteRules");
        }
        [HttpPost("updateStatus")]
        public async Task<BuildingQualityCheckResponse> UpdateQualityStatus(BuildingQualityCheckRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            var executionUser = await _authTokenService.GetUserIdFromToken(token);
            request.ExecutionUser = executionUser;
            return await _buildingQualityCheckService.Execute(request, "UpdateStatus");
        }

    }
}
