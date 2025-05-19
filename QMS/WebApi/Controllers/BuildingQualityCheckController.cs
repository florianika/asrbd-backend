using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Ports;
using Application.Quality.BuildingQualityCheck.Response;
using Application.Quality.BuildingQualityCheck.Request;
using Application.Quality.BuildingQualityCheck;
using Application.Quality.AutomaticRules.Response;
using Application.Quality.AutomaticRules.Request;
using Application.Quality.AutomaticRules;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/check")]
    public class BuildingQualityCheckController : QmsControllerBase
    {
        private readonly IAuthTokenService _authTokenService;
        private readonly BuildingQualityCheck _buildingQualityCheckService;
        private readonly AutomaticRules _automaticRulesService;

        public BuildingQualityCheckController(IAuthTokenService authTokenService, BuildingQualityCheck buildingQualityCheckService, AutomaticRules automaticRules) {
            _authTokenService = authTokenService;
            _buildingQualityCheckService = buildingQualityCheckService;
            _automaticRulesService = automaticRules;
        }
        
        [HttpPost("buildings")]
        public async Task<BuildingQualityCheckResponse> BuildingQualityCheck(BuildingQualityCheckRequest request) {            
            var token = ExtractBearerToken();
            token = token.Replace("Bearer ", "");
            var executionUser = await _authTokenService.GetUserIdFromToken(token);
            request.ExecutionUser = executionUser;
            return await _buildingQualityCheckService.Execute(request);
        }
        [HttpPost("automatic")]
        public async Task<AutomaticRulesResponse> AutomaticRules(AutomaticRulesRequest request)
        {
            var token = ExtractBearerToken();
            var executionUser = await _authTokenService.GetUserIdFromToken(token);
            request.ExecutionUser = executionUser;
            return await _automaticRulesService.Execute(request);
        }

    }
}
