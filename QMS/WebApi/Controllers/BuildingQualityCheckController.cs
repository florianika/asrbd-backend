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
        private readonly BuidlingQualityCheck _buildingQualtyCheckService;

        public BuildingQualityCheckController(IAuthTokenService authTokenService, BuidlingQualityCheck buildingQualityCheckService) {
            _authTokenService = authTokenService;
            _buildingQualtyCheckService = buildingQualityCheckService;
        }
        
        [HttpPost("building/{id}")]
        public async Task<BuildingQualityCheckResponse> BuildingQualityCheck(Guid id, BuildingQualityCheckRequest request) {
            request.BuildingId = id;
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            var executionUser = await _authTokenService.GetUserIdFromToken(token);
            request.ExecutionUser = executionUser;
            return await _buildingQualtyCheckService.Execute(request);
        }

    }
}
