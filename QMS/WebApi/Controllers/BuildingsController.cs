using Application.Building.TestBuildings;
using Application.Building.TestBuildings.Request;
using Application.Building.TestBuildings.Response;
using Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Building.GetBldJobStatus.Response;
using Application.Building.GetBldJobStatus.Request;
using Application.Building.GetBldJobStatus;
using Application.Building;
using Application.Queries.GetBuildingQualityStats.Response;
using Application.Queries.GetDwellingQualityStats.Response;
using Application.Queries.GetBuildingQualityStats;
using Application.Queries.GetDwellingQualityStats;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/qms/buildings")]
    public class BuildingsController : QmsControllerBase
    {
        private readonly ITestBuildings _testBuildingsService;
        private readonly IAuthTokenService _authTokenService;
        private readonly IGetBldJobStatus _getJobStatusService;
        private readonly IGetBuildingQualityStatsQuery _getBuildingQualityStatsQuery;
        private readonly IGetDwellingQualityStatsQuery _getDwellingQualaityStatsQuery;
        public BuildingsController(ITestBuildings testBuildingsService,
            IAuthTokenService authTokenService,
            IGetBldJobStatus getJobStatusService,
            IGetBuildingQualityStatsQuery getBuildingQualityStatsQuery,
            IGetDwellingQualityStatsQuery getDwellingQualityStatsQuery
            )
        {
            _testBuildingsService = testBuildingsService;
            _authTokenService = authTokenService;
            _getJobStatusService = getJobStatusService;
            _getBuildingQualityStatsQuery = getBuildingQualityStatsQuery;
            _getDwellingQualaityStatsQuery = getDwellingQualityStatsQuery;
        }
        [HttpPost]
        [Route("run-test-job/all")]
        public async Task<TestBuildingsResponse> TestAllBuildings([FromBody] TestBuildingRequestDTO dto)
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = true; // all buildings
            request.StartAt = dto?.StartAt;
            return await _testBuildingsService.Execute(request);
        }

        [HttpPost]
        [Route("run-test-job/untested")]
        public async Task<TestBuildingsResponse> TestUntestedBld([FromBody] TestBuildingRequestDTO dto)
        {
            TestBuildingsRequest request = new TestBuildingsRequest();
            var token = ExtractBearerToken();
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            request.isAllBuildings = false; // untested buildings
            request.StartAt = dto?.StartAt;
            return await _testBuildingsService.Execute(request);
        }
        [HttpGet]
        [Route("status-test-job/{id:int}")]
        public async Task<GetBldJobStatusResponse> BldGetJobStatus(int id)
        {
            return await _getJobStatusService.Execute(new GetBldJobStatusRequest() { Id = id });
        }

        [HttpGet]
        [Route("bld-quality-stats")]
        public async Task<GetBuildingQualityStatsResponse> GetBuildingQualityStats()
        {
            return await _getBuildingQualityStatsQuery.Execute();
        }

        [HttpGet]
        [Route("dwl-quality-stats")]
        public async Task<GetDwellingQualityStatsResponse> GetDwellingQualityStats()
        {
            return await _getDwellingQualaityStatsQuery.Execute();
        }
    }
}
